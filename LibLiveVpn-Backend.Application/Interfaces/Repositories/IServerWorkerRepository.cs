using LibLiveVpn_Backend.Domain.Models;

namespace LibLiveVpn_Backend.Application.Interfaces.Repositories
{
    public interface IServerWorkerRepository
    {
        /// <summary>
        /// Функция получения объекта воркера системы по Id
        /// </summary>
        /// <param name="workerId">Идентификатор воркера в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном поиске возвращает объект сервера, иначе Null</returns>
        Task<ServerWorker?> GetByIdAsync(Guid workerId, CancellationToken cancellationToken);

        /// <summary>
        /// Функция получения всех воркеров системы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>Возвращает коллекцию серверов системы</returns>
        Task<IEnumerable<ServerWorker>> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Функция создания воркера с заданными параметрами
        /// </summary>
        /// <param name="serverWorker">Класс с параметрами воркера. После успешного создания в этом классе полю Id присваивается новое уникальное значение воркера в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном создании возвращает объект воркера, иначе Null</returns>
        Task<ServerWorker?> CreateAsync(ServerWorker serverWorker, CancellationToken cancellationToken);

        /// <summary>
        /// Функция обновления воркера с заданными параметрами
        /// </summary>
        /// <param name="serverWorker">Класс с параметрами для обновления воркера. Обновляемые поля выбираются конкретной реализацией. В поле Id должен находится Id сущности, которую необходимо обновить</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном обновлении возвращает обновленный объект воркера, иначе Null</returns>
        Task<ServerWorker?> UpdateAsync(ServerWorker serverWorker, CancellationToken cancellationToken);

        /// <summary>
        /// Функция удаления воркера из системы по Id
        /// </summary>
        /// <param name="workerId">Идентификатор воркера в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном удалении возвращает True, иначе False</returns>
        Task<bool> DeleteAsync(Guid workerId, CancellationToken cancellationToken);
    }
}
