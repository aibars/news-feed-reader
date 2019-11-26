using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsFeedReader.Domain.ApiModels.Request;
using NewsFeedReader.Domain.ApiModels.Response;
using NewsFeedReader.Domain.Models;
using NewsFeedReader.Logic.Interface;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NewsFeedReader.Web.Tests
{
    public class AccountControllerTest
    {
        private FakeUserManager _userManager;
        private FakeSignInManager _signInManager;
        private Mock<IMapper> _mapper;
        private Mock<ITokenService> _tokenService;
        private Mock<IUserStore<ApplicationUser>> _userStore;

        public AccountControllerTest()
        {
            _userStore = new Mock<IUserStore<ApplicationUser>>();


            _mapper = new Mock<IMapper>();
            _tokenService = new Mock<ITokenService>();
        }

        [Fact]
        public void WhenLoginReturnsLoggedInUser()
        {
            // Arrange
            var model = new LoginRequestDto
            {
                Password = "123",
                UserName = "user"
            };

            var loggedInUser = new LoggedInUserDto
            {
                UserName = "user"
            };

            var users = new List<ApplicationUser> {
                new ApplicationUser { UserName = "user" } }.AsQueryable();
            _userManager = new FakeUserManager(_userStore, users);
            _signInManager = new FakeSignInManager(_userStore, users, Microsoft.AspNetCore.Identity.SignInResult.Success);

            _tokenService.Setup(x => x.GenerateJwtToken(It.IsAny<ApplicationUser>())).Returns(new Logic.Models.JsonWebToken("user", "123", 1));
            _mapper.Setup(x => x.Map<LoggedInUserDto>(It.IsAny<ApplicationUser>())).Returns(loggedInUser);

            var controller = new AccountController(_userManager, _signInManager, _mapper.Object, _tokenService.Object);

            // Act
            OkObjectResult result = controller.Login(model).Result as OkObjectResult;

            var resultModel = result.Value as LoggedInUserDto;

            // Assert
            Assert.Equal("user", resultModel.UserName);
        }

        [Fact]
        public void WhenLoginReturnsAnError()
        {
            // Arrange
            var model = new LoginRequestDto
            {
                Password = "123",
                UserName = "user"
            };

            var loggedInUser = new LoggedInUserDto
            {
                UserName = "user"
            };

            var users = new List<ApplicationUser> {
                new ApplicationUser { UserName = "user" } }.AsQueryable();
            _userManager = new FakeUserManager(_userStore, users);
            _signInManager = new FakeSignInManager(_userStore, users, Microsoft.AspNetCore.Identity.SignInResult.Failed);

            _tokenService.Setup(x => x.GenerateJwtToken(It.IsAny<ApplicationUser>())).Returns(new Logic.Models.JsonWebToken("user", "123", 1));
            _mapper.Setup(x => x.Map<LoggedInUserDto>(It.IsAny<ApplicationUser>())).Returns(loggedInUser);

            var controller = new AccountController(_userManager, _signInManager, _mapper.Object, _tokenService.Object);

            // Act
            var result = controller.Login(model).Result;

            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public void WhenRegisterReturnsLoggedInUser()
        {
            // Arrange
            var model = new RegisterRequestDto
            {
                Password = "123",
                UserName = "user"
            };

            var loggedInUser = new LoggedInUserDto
            {
                UserName = "user"
            };

            var appUser = new ApplicationUser
            {
                UserName = "user"
            };

            var users = new List<ApplicationUser>().AsQueryable();
            _userManager = new FakeUserManager(_userStore, users);

            _signInManager = new FakeSignInManager(_userStore, users, Microsoft.AspNetCore.Identity.SignInResult.Success);

            _tokenService.Setup(x => x.GenerateJwtToken(It.IsAny<ApplicationUser>())).Returns(new Logic.Models.JsonWebToken("user", "123", 1));
            _mapper.Setup(x => x.Map<ApplicationUser>(It.IsAny<RegisterRequestDto>())).Returns(appUser);
            _mapper.Setup(x => x.Map<LoggedInUserDto>(It.IsAny<ApplicationUser>())).Returns(loggedInUser);

            var controller = new AccountController(_userManager, _signInManager, _mapper.Object, _tokenService.Object);

            // Act
            OkObjectResult result = controller.Register(model).Result as OkObjectResult;

            var resultModel = result.Value as Logic.Models.JsonWebToken;

            // Assert
            Assert.Equal("123", resultModel.Token);
        }

        [Fact]
        public void WhenRegisterReturnsAnErrorBecauseUserAlreadyExists()
        {
            // Arrange
            var model = new RegisterRequestDto
            {
                Password = "123",
                UserName = "user"
            };

            var loggedInUser = new LoggedInUserDto
            {
                UserName = "user"
            };

            var appUser = new ApplicationUser
            {
                UserName = "user"
            };

            var users = new List<ApplicationUser> {
                new ApplicationUser { UserName = "user" } }.AsQueryable();

            _userManager = new FakeUserManager(_userStore, users);

            _signInManager = new FakeSignInManager(_userStore, users, Microsoft.AspNetCore.Identity.SignInResult.Failed);

            _tokenService.Setup(x => x.GenerateJwtToken(It.IsAny<ApplicationUser>())).Returns(new Logic.Models.JsonWebToken("a", "123", 1));
            _mapper.Setup(x => x.Map<ApplicationUser>(It.IsAny<RegisterRequestDto>())).Returns(appUser);
            _mapper.Setup(x => x.Map<LoggedInUserDto>(It.IsAny<ApplicationUser>())).Returns(loggedInUser);

            var controller = new AccountController(_userManager, _signInManager, _mapper.Object, _tokenService.Object);

            // Act
            var result = controller.Register(model).Result as BadRequestObjectResult;

            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }
    }
}
