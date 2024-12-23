using LibLiveVpn_Backend.Domain.Models;

namespace LibLiveVpn_Backend.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Функция получения пользователя системы по Id
        /// </summary>
        /// <param name="userId">Идентификатор пользователя в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном поиске возвращает объект пользователя системы, иначе Null</returns>
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Функция получения всех пользователей системы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>Возвращает коллекцию пользователей системы</returns>
        Task<IEnumerable<User>> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Функция создания в системе пользователя с заданными параметрами
        /// </summary>
        /// <param name="user">Класс с параметрами пользователя. После успешного создания в этом классе полю Id присваивается новое уникальное значение пользователя в системе</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном создании возвращает объект пользователя системы, иначе Null</returns>
        Task<User?> CreateAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Функция обновления в базе данных пользователя с заданными параметрами
        /// </summary>
        /// <param name="user">Класс с параметрами для обновления пользователя. Обновляемые поля выбираются конкретной реализацией. В поле Id должен находится Id сущности, которую необходимо обновить</param>
        /// <param name="cancellationToken">Токен отмены асинхронной функции</param>
        /// <returns>При успешном обновлении возвращает обновленный объект пользователя системы, иначе Null</returns>
        Task<User?> UpdateAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Функция удаления пользователя из системы по Id
        /// </summary>
        /// <param name="userId">Идентификатор пользователя в системе</param>
        /// <returns>При успешном удалении возвращает True, иначе False</returns>
        bool Delete(Guid userId);
    }
}
