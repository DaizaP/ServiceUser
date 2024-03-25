using ServiceUser.Models.Dto;
using ServiceUser.Models;

namespace ServiceUser.Abstraction
{
    public interface IUserAuthentificateService
    {
        string Authentificate(LoginModelDto loginModel);
    }
}
