using Flunt.Notifications;
using Moq;
using System.Collections.Generic;
using System.Threading;
using WorkShop.Commands;
using WorkShop.DTOs;
using WorkShop.Entities;
using WorkShop.Handlers;
using WorkShop.Repositories;
using WorkShop.ValueObjects;
using Xunit;

namespace WorkShopTestes.Handlers
{
    public class UserHandlerTest
    {
        private readonly UserHandler _userHandler;
        private readonly Mock<IUserRepository> _mockedUserRepository;

        public UserHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _userHandler = new UserHandler(_mockedUserRepository.Object);
        }


        [Fact]
        public void LoginUserCommand_WhenCommandIsInvalid_ShouldReturnError()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "usuario.com", Password = "123456" };

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<List<Notification>>(result.Data);
        }


        [Fact]
        public void LoginUserCommand_WhenUserIsNull_ShouldReturnError()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "usuario@email.com", Password = "123456" };

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>()));

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
            Assert.Null(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LoginUserCommand_WhenUserAreNotActive_ShouldReturnError()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "usuario@email.com", Password = "123456" };
            var user = new User() { IsActive = false, Name = "Usuario1" };

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
            Assert.Null(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LoginUserCommand_WhenCompletedRegistrationAreFalse_ShouldReturnError()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "usuario@email.com", Password = "123456" };
            var user = new User() { IsActive = true, Name = "Usuario1", CompletedRegistration = false };

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<string>(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LoginUserCommand_WhenPasswordAreDiferent_ShouldReturnError()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "usuario@email.com", Password = "123456" };
            var passwordUser = new Password("12345533367");

            var user = new User() { IsActive = true, Name = "Usuario1", CompletedRegistration = true, Password = passwordUser };

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<string>(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LoginUserCommand_ShouldReturnSuccess()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "usuario@email.com", Password = "123456" };
            var passwordUser = new Password("123456");

            var user = new User() { IsActive = true, Name = "Usuario1", CompletedRegistration = true, Password = passwordUser };

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<UserDTO>(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

    }
}
