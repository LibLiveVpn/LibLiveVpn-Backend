using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using LibLiveVpn_Backend.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibLiveVpn_Backend.Persistence.Repositories
{
    public class UserRepositoryEfCore : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepositoryEfCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .Select(e => new User
                {
                    Id = e.Id,
                    Login = e.Login,
                    Name = e.Name,
                    Description = e.Description,
                    Configurations = new()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                .Select(e => new User
                {
                    Id = e.Id,
                    Login = e.Login,
                    Name = e.Name,
                    Description = e.Description,
                    Configurations = new()
                })
                .FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);
        }

        public async Task<User?> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var existedUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == user.Id && e.Login == user.Login, cancellationToken);
            if (existedUser != null)
            {
                return null;
            }

            user.Id = Guid.NewGuid();

            var userEntity = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Description = user.Description
            };

            await _context.Users.AddAsync(userEntity, cancellationToken);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return user;
        }

        public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var existedUser = await _context.Users.FindAsync(user.Id, cancellationToken);
            if (existedUser == null)
            {
                return null;
            }

            existedUser.Name = user.Name;
            existedUser.Description = user.Description;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return user;
        }

        public async Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken)
        {
            var existedUser = await _context.Users.FindAsync(userId, cancellationToken);
            if (existedUser == null)
            {
                return false;
            }

            _context.Users.Remove(existedUser);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
