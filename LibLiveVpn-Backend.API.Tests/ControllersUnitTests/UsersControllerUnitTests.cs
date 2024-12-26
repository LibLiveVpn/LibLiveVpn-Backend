using LibLiveVpn_Backend.API.Controllers;
using LibLiveVpn_Backend.API.Models;
using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibLiveVpn_Backend.UnitTests.ControllersUnitTests
{
    public class UsersControllerUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetUserReturnNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync((User?)null);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.GetUser(userId, CancellationToken.None);

            // Assert
            Assert.Fail();
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetUserReturnUserObject()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = "User",
                Name = "Test",
                Description = "Test",
                Configurations = new List<VpnConfigurationSummary>()
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.GetByIdAsync(user.Id, CancellationToken.None)).ReturnsAsync(user);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.GetUser(user.Id, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.That(okResult.Value, Is.EqualTo(user));
        }

        [Test]
        public async Task GetUsersReturnEmptyCollection()
        {
            // Arrange
            var emptyUserList = new List<User>();
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.GetAsync(CancellationToken.None)).ReturnsAsync(emptyUserList);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.GetAllUsers(CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<User>>(okResult.Value);

            var resultList = okResult.Value as List<User>;
            Assert.IsNotNull(resultList);
            Assert.That(resultList.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetUsersReturn4AmountCollection()
        {
            // Arrange
            var userList = new List<User>()
            {
                new User { Id = Guid.NewGuid(), Login = "User1", Name = "Test", Description = "Test", Configurations = new() },
                new User { Id = Guid.NewGuid(), Login = "User2", Name = "Test", Description = "Test", Configurations = new() },
                new User { Id = Guid.NewGuid(), Login = "User3", Name = "Test", Description = "Test", Configurations = new() },
                new User { Id = Guid.NewGuid(), Login = "User4", Name = "Test", Description = "Test", Configurations = new() },
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.GetAsync(CancellationToken.None)).ReturnsAsync(userList);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.GetAllUsers(CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<User>>(okResult.Value);

            var resultList = okResult.Value as List<User>;
            Assert.IsNotNull(resultList);
            Assert.That(resultList.Count, Is.EqualTo(4));
        }

        [Test]
        public async Task CreateUserReturnBadRequest()
        {
            // Arrange
            var newUserDto = new CreateUserDto
            {
                Login = "User",
                Name = "Test",
                Description = "Test"
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.CreateAsync(It.IsAny<User>(), CancellationToken.None)).ReturnsAsync((User?)null);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.CreateUser(newUserDto, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task CreateUserReturnCreatedUserObject()
        {
            // Arrange
            var newUserDto = new CreateUserDto
            {
                Login = "User",
                Name = "Test",
                Description = "Test"
            };

            var createdUser = new User
            {
                Id = Guid.NewGuid(),
                Login = newUserDto.Login,
                Name = newUserDto.Name,
                Description = newUserDto.Description,
                Configurations = new()
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.CreateAsync(It.IsAny<User>(), CancellationToken.None)).ReturnsAsync(createdUser);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.CreateUser(newUserDto, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);

            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Value);
            Assert.That(createdResult.Value, Is.EqualTo(createdUser));
        }

        [Test]
        public async Task UpdateUserReturnBadRequest()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test"
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.UpdateAsync(It.IsAny<User>(), CancellationToken.None)).ReturnsAsync((User?)null);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.UpdateUser(updateUserDto, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestObjectResult);
            Assert.IsNotNull(badRequestObjectResult.Value);
            Assert.That(badRequestObjectResult.Value, Is.EqualTo("User not exists"));
        }

        [Test]
        public async Task UpdateUserReturnUpdatedUserObject()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test"
            };

            var updatedUser = new User
            {
                Id = updateUserDto.Id,
                Login = "User",
                Name = updateUserDto.Name,
                Description = updateUserDto.Description,
                Configurations = new()
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(updatedUser);
            mock.Setup(repo => repo.UpdateAsync(It.IsAny<User>(), CancellationToken.None)).ReturnsAsync(updatedUser);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.UpdateUser(updateUserDto, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.That(okResult.Value, Is.EqualTo(updatedUser));
        }

        [Test]
        public async Task DeleteUserReturnBadRequest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(false);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.DeleteUser(userId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task DeleteUserReturnOkResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(true);
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.DeleteUser(userId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
