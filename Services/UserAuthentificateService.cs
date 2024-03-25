using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ServiceUser.Abstraction;
using ServiceUser.Models;
using ServiceUser.Models.Context;
using ServiceUser.Models.Dto;

namespace ServiceUser.Services
{
    public class UserAuthentificateService : IUserAuthentificateService
    {
        private readonly GenerateToken _generateToken;
        readonly private ServiceUserDbContext _context;
        public UserAuthentificateService(ServiceUserDbContext context, IConfiguration config)
        {
            _context = context;
            _generateToken = new GenerateToken(config);
        }
        public string Authentificate(LoginModelDto login)
        {
            var user = _context.Users.FirstOrDefault(x =>
            x.Password == PasswordService.GetHash(login.Password)
            && x.Email == login.Email);
            if(user != null)
            {
                var role = _context.Roles.FirstOrDefault(x =>
                x.RoleId == user.RoleId);
                var token = _generateToken.GetToken(user, role);
                return token;
            }
            return null;
        }
    }
}
