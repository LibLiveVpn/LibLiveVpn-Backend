using LibLiveVpn_Backend.Domain.Models;

namespace LibLiveVpn_Backend.Application.Interfaces.Repositories
{
    public interface IServerRepository
    {
        /// <summary>
        /// Функция получения объекта сервера системы по Id
        /// </summary>
        /// <param name="serverId">Идентификатор сервера в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном поиске возвращает объект сервера, иначе Null</returns>
        Task<Server?> GetServerByIdAsync(Guid serverId, CancellationToken cancellationToken);

        /// <summary>
        /// Функция получения всех серверов системы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>Возвращает коллекцию серверов системы</returns>
        Task<IEnumerable<Server>> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Функция создания сервера с заданными параметрами
        /// </summary>
        /// <param name="server">Класс с параметрами сервера. После успешного создания в этом классе полю Id присваивается новое уникальное значение сервера в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном создании возвращает объект сервера, иначе Null</returns>
        Task<Server?> CreateAsync(Server server, CancellationToken cancellationToken);

        /// <summary>
        /// Функция удаления сервера из системы по Id
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns>При успешном удалении возвращает True, иначе False</returns>
        bool Delete(Guid serverId);
    }
}
