using LibLiveVpn_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibLiveVpn_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly IVpnConnectionService _vpnConnectionService;

        public ConnectionsController(IVpnConnectionService vpnConnectionService)
        {
            _vpnConnectionService = vpnConnectionService;
        }

        /// <summary>
        /// Метод инициализации подключения пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает 200 и id подключения при успехе, иначе 201</returns>
        [HttpPost]
        public async Task<ActionResult> ConnectUser(Guid userId, CancellationToken cancellationToken)
        {
            var connectionId = await _vpnConnectionService.ConnectUserAsync(userId, cancellationToken);
            if (connectionId == default)
            {
                return Ok();
            }

            return Ok(connectionId);
        }
    }
}
