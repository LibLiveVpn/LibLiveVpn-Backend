using LibLiveVpn_Backend.API.Controllers;
using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibLiveVpn_Backend.UnitTests.ControllersUnitTests
{
    public class ServersControllerUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetServerReturnNotFound()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var mock = new Mock<IServerRepository>();
            mock.Setup(repo => repo.GetByIdAsync(serverId, CancellationToken.None)).ReturnsAsync((Server?)null);
            var controller = new ServersController(mock.Object);

            // Act
            var result = await controller.GetServer(serverId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetServerReturnOkWithServerObject()
        {
            // Arrange
            var server = new Server
            {
                Id = Guid.NewGuid(),
                Ip = "127.0.0.1",
                Port = 1010,
                Provider = "Test",
                Name = "Test",
                Description = "Test"
            };
            var mock = new Mock<IServerRepository>();
            mock.Setup(repo => repo.GetByIdAsync(server.Id, CancellationToken.None)).ReturnsAsync(server);
            var controller = new ServersController(mock.Object);

            // Act
            var result = await controller.GetServer(server.Id, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.That(okResult.Value, Is.EqualTo(server));
        }

        [Test]
        public async Task GetServersReturnEmptyCollection()
        {
            // Arrange
            var mock = new Mock<IServerRepository>();
            var emptyServerList = new List<Server>();
            mock.Setup(repo => repo.GetAsync(CancellationToken.None)).ReturnsAsync(emptyServerList);
            var controller = new ServersController(mock.Object);

            // Act
            var result = await controller.GetAllServers(CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Server>>(okResult.Value);

            var resultList = okResult.Value as List<Server>;
            Assert.IsNotNull(resultList);
            Assert.That(resultList.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetServersReturn4AmountCollection()
        {
            // Arrange
            var mock = new Mock<IServerRepository>();
            var serverList = new List<Server>()
            {
                new Server { Id = Guid.NewGuid(), Ip = "127.0.0.1", Port = 1011, Provider = "Test", Name = "Test", Description = "Test" },
                new Server { Id = Guid.NewGuid(), Ip = "127.0.0.2", Port = 1012, Provider = "Test", Name = "Test", Description = "Test" },
                new Server { Id = Guid.NewGuid(), Ip = "127.0.0.3", Port = 1013, Provider = "Test", Name = "Test", Description = "Test" },
                new Server { Id = Guid.NewGuid(), Ip = "127.0.0.4", Port = 1014, Provider = "Test", Name = "Test", Description = "Test" },
            };
            mock.Setup(repo => repo.GetAsync(CancellationToken.None)).ReturnsAsync(serverList);
            var controller = new ServersController(mock.Object);

            // Act
            var result = await controller.GetAllServers(CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Server>>(okResult.Value);

            var resultList = okResult.Value as List<Server>;
            Assert.IsNotNull(resultList);
            Assert.That(resultList.Count, Is.EqualTo(4));
        }
    }
}
