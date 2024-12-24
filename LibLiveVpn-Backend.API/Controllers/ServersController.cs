using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibLiveVpn_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerRepository _serverRepository;

        public ServersController(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        /// <summary>
        /// Метод получения сервера по Id
        /// </summary>
        /// <param name="id">Идентификатор сервера</param>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает 200 и объект сервера в случае успеха, иначе 204</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServer(Guid id, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetByIdAsync(id, cancellationToken);
            return Ok(server);
        }

        /// <summary>
        /// Метод получения всех серверов системы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает 200 со списком всех серверов</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllServers(CancellationToken cancellationToken)
        {
            var servers = await _serverRepository.GetAsync(cancellationToken);
            return Ok(servers);
        }
    }
}
