using LibLiveVpn_Backend.Domain.Models;

namespace LibLiveVpn_Backend.Application.Interfaces.Repositories
{
    public interface IWorkerCommandTaskRepository
    {
        /// <summary>
        /// Функция получения объекта выполняемой команды
        /// </summary>
        /// <param name="commandTaskId">Идентификатор выполняемой команды</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном поиске возвращает объект выполняемой команды, иначе Null</returns>
        Task<WorkerCommandTask?> GetByIdAsync(Guid commandTaskId, CancellationToken cancellationToken);

        /// <summary>
        /// Функция получения всех выполняемых команд
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>Возвращает коллекцию выполняемых команд</returns>
        Task<IEnumerable<WorkerCommandTask>> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Функция создания выполняемой команды с заданными параметрами
        /// </summary>
        /// <param name="server">Класс с параметрами выполняемой команды. После успешного создания в этом классе полю Id присваивается новое уникальное значение выполняемой команды</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном создании возвращает объект выполняемой команды, иначе Null</returns>
        Task<WorkerCommandTask?> CreateAsync(WorkerCommandTask server, CancellationToken cancellationToken);

        /// <summary>
        /// Функция обновления выполняемой команды с заданными параметрами
        /// </summary>
        /// <param name="server">Класс с параметрами для обновления выполняемой команды. Обновляемые поля выбираются конкретной реализацией. В поле Id должен находится Id сущности, которую необходимо обновить</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном обновлении возвращает обновленный объект выполняемой команды, иначе Null</returns>
        Task<WorkerCommandTask?> UpdateAsync(WorkerCommandTask server, CancellationToken cancellationToken);
    }
}
