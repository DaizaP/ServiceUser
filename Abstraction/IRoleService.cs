using ServiceUser.Models;
using ServiceUser.Models.Dto;

namespace ServiceUser.Abstraction
{
    public interface IRoleService
    {
        public IEnumerable<RoleModelDto> GetRoles();
    }
}
