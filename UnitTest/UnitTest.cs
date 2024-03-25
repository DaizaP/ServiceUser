using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceUser.Abstraction;
using ServiceUser.Controllers;
using ServiceUser.Models;
using ServiceUser.Models.Dto;
using ServiceUser.Services;
using XAct;
using Xunit;

namespace UnitTestsServiceUser
{
    public class UserControllerTests : ControllerBase
    {
        [Fact]
        public async Task GetUser_ReturnsUser()
        {
            // �����������
            IEnumerable<GetUserDto> getUserDtos = new List<GetUserDto> { new GetUserDto { Id = new Guid("6fd7a6b1-493c-4647-81c2-a1457dda1b1f"), Email = "admin@fake.ru", RoleId = RoleId.Admin, UserName = "admin" } };
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetUsers()).Returns(getUserDtos);
            var controller = new UserController(mockUserService.Object);

            // ��������
            var result = controller.GetUsers();

            // �����������
            var usersResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsType<List<GetUserDto>>(usersResult.Value);
            var user = users.FirstOrDefault();
            Assert.Equal(new Guid("6fd7a6b1-493c-4647-81c2-a1457dda1b1f"), user.Id);
            Assert.Equal("admin", user.UserName);
        }
        [Fact]
        public void AddUser_ReturnsGuid()
        {
            // �����������
            AddUserModelDto addUserModel = new AddUserModelDto() { Email = "admin@fake.ru", UserName = "admin", Password = "Qwerty123" };
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.AddUser(addUserModel)).Returns(new Guid("6fd7a6b1-493c-4647-81c2-a1457dda1b1f"));
            var controller = new UserController(mockUserService.Object);

            // ��������
            var result = controller.AddUser(addUserModel);

            // �����������
            var guidResult = Assert.IsType<OkObjectResult>(result);
            var guid = Assert.IsType<Guid>(guidResult.Value);
            Assert.Equal(new Guid("6fd7a6b1-493c-4647-81c2-a1457dda1b1f"), guid);
        }
        [Fact]
        public void GetRoles_ReturnRoles()
        {
            // �����������
            IEnumerable<RoleModelDto> getRoles = new List<RoleModelDto> { new RoleModelDto { Id = 0, RoleName = "Admin", RoleId = RoleId.Admin }, new RoleModelDto { Id = 1, RoleName = "User", RoleId = RoleId.User } };
            var mockUserService = new Mock<IRoleService>();
            mockUserService.Setup(service => service.GetRoles()).Returns(getRoles);
            var controller = new RoleController(mockUserService.Object);

            // ��������
            var result = controller.GetRoles();

            // �����������
            var rolesResult = Assert.IsType<OkObjectResult>(result);
            var roles = Assert.IsType<List<RoleModelDto>>(rolesResult.Value);
            Assert.Equal(2, roles.Count);
            Assert.Equal(true, roles.Any(x => x.RoleName == "Admin"));
            Assert.Equal(false, roles.Any(x => x.RoleName == "Admi"));
        }
        [Fact]
        public void Login_ReturnIActionResult()
        {
            // �����������
            LoginModelDto login = new LoginModelDto {Email = "admin@fake.ru", Password = "Qwerty123" };
            var mockUserService = new Mock<IUserAuthentificateService>();
            mockUserService.Setup(service => service.Authentificate(login)).Returns("token");
            var controller = new LoginController(mockUserService.Object);

            // ��������
            var result = controller.Login(login);

            // �����������
            var objResult = Assert.IsType<OkObjectResult>(result);
            var res = Assert.IsType<string>(objResult.Value);
            Assert.Equal("token", res);
        }
    }
}