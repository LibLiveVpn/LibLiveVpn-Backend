using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;

namespace LibLiveVpn_Backend.Persistence.Repositories
{
    public class ServerRepositoryEfCore : IServerRepository
    {
        private readonly ApplicationDbContext _context;

        public ServerRepositoryEfCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Server>> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Server?> GetByIdAsync(Guid serverId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Server?> CreateAsync(Server server, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Server?> UpdateAsync(Server server, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid serverId)
        {
            throw new NotImplementedException();
        }
    }
}
