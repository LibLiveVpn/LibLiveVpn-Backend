using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;

namespace LibLiveVpn_Backend.Persistence.Repositories
{
    public class UserRepositoryEfCore : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepositoryEfCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<User>> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
