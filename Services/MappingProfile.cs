using AutoMapper;
using ServiceUser.Models;
using ServiceUser.Models.Dto;

namespace ServiceUser.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, AddUserModelDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<RoleModel, RoleModelDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<UserModel, UserModelDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<UserModel, GetUserDto>(MemberList.Destination);
        }
    }
}
