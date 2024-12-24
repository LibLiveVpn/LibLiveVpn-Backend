using LibLiveVpn_Backend.API.Models;
using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibLiveVpn_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Метод получения пользователя по Id
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает 200 и объект пользователя в случае успеха, иначе 204</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Метод получения всех пользователей
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает 200 со списком всех пользователей</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync(cancellationToken);
            return Ok(users);
        }

        /// <summary>
        /// Метод создание нового пользователя
        /// </summary>
        /// <param name="createUserDto">Модель с параметрами для создания пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает объект созданного пользователя в случае успеха. В случае ошибки при создании возвращает код 204</returns>
        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Login = createUserDto.Login,
                Name = createUserDto.Name,
                Description = createUserDto.Description
            };

            newUser = await _userRepository.CreateAsync(newUser, cancellationToken);
            if (newUser == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetUser), newUser);
        }

        /// <summary>
        /// Метод обновления пользователя
        /// </summary>
        /// <param name="updateUserDto">Модель с параметрами для обновления пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронного метода</param>
        /// <returns>Возвращает объект обновленного пользователя в случае успеха. В случае отсутствия пользователя с заданным Id в системе возвращает BadRequest. В случае ошибки при обновлениии возвращает код 204</returns>
        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var updatingUser = await _userRepository.GetByIdAsync(updateUserDto.Id, cancellationToken);
            if (updatingUser == null)
            {
                return BadRequest("User not exists");
            }

            updatingUser.Name = updateUserDto.Name;
            updatingUser.Description = updateUserDto.Description;

            updatingUser = await _userRepository.UpdateAsync(updatingUser, cancellationToken);

            return Ok(updatingUser);
        }

        /// <summary>
        /// Метод удаления пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>В случае успешного удаления возвращает код 201, иначе BadRequest </returns>
        [HttpDelete]
        public ActionResult DeleteUser(Guid id)
        {
            if (!_userRepository.Delete(id))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
