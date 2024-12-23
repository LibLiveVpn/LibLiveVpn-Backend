namespace LibLiveVpn_Backend.Application.Interfaces
{
    public interface IVpnConnectionService
    {
        /// <summary>
        /// Функция подключения пользователя к серверу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>Возвращает Id подключения в случае успеха, иначе default значение</returns>
        Task<Guid> ConnectUserAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Функция подключения пользователя к определенному серверу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя в системе</param>
        /// <param name="serverId">Идентификатор сервера в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>Возвращает Id подключения в случае успеха, иначе default значение</returns>
        Task<Guid> ConnectUserWithServerAsync(Guid userId, Guid serverId, CancellationToken cancellationToken);
    }
}
