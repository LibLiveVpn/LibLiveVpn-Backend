using LibLiveVpn_Backend.API.Controllers;
using LibLiveVpn_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibLiveVpn_Backend.UnitTests.ControllersUnitTests
{
    public class ConnectionsControllerUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CannotConnectUserResultOkNoContent()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mock = new Mock<IVpnConnectionService>();
            mock.Setup(repo => repo.ConnectUserAsync(userId, CancellationToken.None)).ReturnsAsync(Guid.Empty);
            var controller = new ConnectionsController(mock.Object);

            // Act
            var result = await controller.ConnectUser(userId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task ConnectUserResultOkWithConnectionId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userConnectionId = Guid.NewGuid();
            var mock = new Mock<IVpnConnectionService>();
            mock.Setup(repo => repo.ConnectUserAsync(userId, CancellationToken.None)).ReturnsAsync(userConnectionId);
            var controller = new ConnectionsController(mock.Object);

            // Act
            var result = await controller.ConnectUser(userId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.That(okResult!.Value, Is.EqualTo(userConnectionId));
        }


    }
}
