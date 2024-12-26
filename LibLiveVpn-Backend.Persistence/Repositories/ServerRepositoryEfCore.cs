using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using LibLiveVpn_Backend.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibLiveVpn_Backend.Persistence.Repositories
{
    public class ServerRepositoryEfCore : IServerRepository
    {
        private readonly ApplicationDbContext _context;

        public ServerRepositoryEfCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Server>> GetAsync(CancellationToken cancellationToken)
        {
            return await _context.Servers.AsNoTracking()
                .Select(e => new Server
                {
                    Id = e.Id,
                    Ip = e.Ip,
                    Port = e.Port,
                    Provider = e.Provider,
                    Name = e.Name,
                    Description = e.Description,
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<Server?> GetByIdAsync(Guid serverId, CancellationToken cancellationToken)
        {
            return await _context.Servers.AsNoTracking()
                .Select(e => new Server
                {
                    Id = e.Id,
                    Ip = e.Ip,
                    Port = e.Port,
                    Provider = e.Provider,
                    Name = e.Name,
                    Description = e.Description,
                })
                .FirstOrDefaultAsync(e => e.Id == serverId, cancellationToken);
        }

        public async Task<Server?> CreateAsync(Server server, CancellationToken cancellationToken)
        {
            server.Id = Guid.NewGuid();

            var serverEntity = new ServerEntity
            {
                Id = server.Id,
                Ip = server.Ip,
                Port = server.Port,
                Provider = server.Provider,
                Name = server.Name,
                Description = server.Description
            };

            await _context.Servers.AddAsync(serverEntity, cancellationToken);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return server;
        }

        public async Task<Server?> UpdateAsync(Server server, CancellationToken cancellationToken)
        {
            var existedServer = await _context.Servers.FindAsync(server.Id, cancellationToken);
            if (existedServer == null)
            {
                return null;
            }

            existedServer.Name = server.Name;
            existedServer.Description = server.Description;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return server;
        }

        public async Task<bool> DeleteAsync(Guid serverId, CancellationToken cancellationToken)
        {
            var existedServer = await _context.Servers.FindAsync(serverId, cancellationToken);
            if (existedServer == null)
            {
                return false;
            }

            _context.Servers.Remove(existedServer);

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
