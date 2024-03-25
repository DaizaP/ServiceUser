using ServiceUser.Models.Dto;

namespace ServiceUser.Abstraction
{
    public interface IUserService
    {
        public Guid AddUser(AddUserModelDto user);
        public bool DeleteUser(string userEmail, Guid adminId);
        public IEnumerable<GetUserDto> GetUsers();
    }
}
