using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServiceUser.Abstraction;
using ServiceUser.Models;
using ServiceUser.Models.Context;
using ServiceUser.Models.Dto;
using ServiceUser.Services;
using System.Xml.Linq;
using XAct;

namespace ServiceUser.Services
{
    public class UserService : IUserService
    {
        readonly private IMapper _mapper;
        readonly private IMemoryCache _memoryCache;
        readonly private ServiceUserDbContext _context;
        public UserService(IMapper mapper, IMemoryCache memoryCache, ServiceUserDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _memoryCache = memoryCache;
        }

        public Guid AddUser(AddUserModelDto user)
        {
            var roleId = new RoleId();

            if (_context.Users.Count() == 0)
                roleId = RoleId.Admin;
            else
                roleId = RoleId.User;

            var entityUser = _context.Users
                .FirstOrDefault(
                x => x.Email.ToLower() == user.Email.ToLower());
            if (entityUser == null)
            {
                if (PasswordService.DifficultyCheck(user.Password) && EmailService.CheckEmailFormat(user.Email))
                {
                    user.Password = PasswordService.GetHash(user.Password);
                    entityUser = _mapper.Map<UserModel>(user);
                    entityUser.RoleId = roleId;
                    _context.Users.Add(entityUser);
                    _context.SaveChanges();
                    _memoryCache.Remove("users");
                    return entityUser.Id;
                }
                else
                {
                    throw new FormatException("Проверьте правильность написания почты и пароля." +
                        "Пароль должен быть длиной от 8 до 30 символов, содержать цифры, заглавные и прописные буквы");
                }
            }
            else
            {
                throw new Exception("Такой пользователь уже добавлен");
            }
        }

        public bool DeleteUser(string userEmail, Guid adminId)
        {
            var user = _context.Users
                .FirstOrDefault(
                x => x.Email.ToLower() == userEmail.ToLower());
            if (user != null)
            {
                if(user.Id == adminId && adminId != null)
                {
                    return false;
                }
                _context.Users.Where(x => x == user).ExecuteDelete();
                _memoryCache.Remove("users");
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<GetUserDto> GetUsers()
        {
            if (_memoryCache.TryGetValue("users", out List<GetUserDto> users))
            {
                return users;
            }

            var userList = _context.Users.Select(x => _mapper.Map<GetUserDto>(x)).ToList();
            _memoryCache.Set("users", userList, TimeSpan.FromMinutes(30));
            return userList;
        }
    }
}
