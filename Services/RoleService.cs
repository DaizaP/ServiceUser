using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ServiceUser.Abstraction;
using ServiceUser.Models;
using ServiceUser.Models.Context;
using ServiceUser.Models.Dto;

namespace ServiceUser.Services
{
    public class RoleService : IRoleService
    {
        readonly private IMapper _mapper;
        readonly private ServiceUserDbContext _context;
        readonly private IMemoryCache _memoryCache;
        public RoleService(IMapper mapper,IMemoryCache memoryCache, ServiceUserDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _memoryCache = memoryCache;
        }
        public IEnumerable<RoleModelDto> GetRoles()
        {
            if (_memoryCache.TryGetValue("roles", out List<RoleModelDto> roles))
            {
                return roles;
            }
            var rolesList = _context.Roles.Select(x => _mapper.Map<RoleModelDto>(x)).ToList();
            _memoryCache.Set("roles", rolesList, TimeSpan.FromMinutes(30));
            return rolesList;
        }
    }
}
