using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using LibLiveVpn_Backend.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibLiveVpn_Backend.Persistence.Repositories
{
    public class ServerWorkerRepositoryEfCore : IServerWorkerRepository
    {
        private readonly ApplicationDbContext _context;

        public ServerWorkerRepositoryEfCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServerWorker>> GetAsync(CancellationToken cancellationToken)
        {
            return await _context.ServerWorkers.AsNoTracking()
                .Select(e => new ServerWorker
                {
                    Id = e.Id,
                    Ip = e.Ip,
                    Name = e.Name,
                    Description = e.Description
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<ServerWorker?> GetByIdAsync(Guid workerId, CancellationToken cancellationToken)
        {
            return await _context.ServerWorkers.AsNoTracking()
                .Select(e => new ServerWorker
                {
                    Id = e.Id,
                    Ip = e.Ip,
                    Name = e.Name,
                    Description = e.Description
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ServerWorker?> CreateAsync(ServerWorker serverWorker, CancellationToken cancellationToken)
        {
            serverWorker.Id = Guid.NewGuid();

            var serverWorkerEntity = new ServerWorkerEntity
            {
                Id = serverWorker.Id,
                Ip = serverWorker.Ip,
                Name = serverWorker.Name,
                Description = serverWorker.Description
            };

            await _context.ServerWorkers.AddAsync(serverWorkerEntity, cancellationToken);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return serverWorker;
        }

        public async Task<ServerWorker?> UpdateAsync(ServerWorker serverWorker, CancellationToken cancellationToken)
        {
            var existedWorker = await _context.ServerWorkers.FindAsync(serverWorker.Id, cancellationToken);
            if (existedWorker == null)
            {
                return null;
            }

            existedWorker.Name = serverWorker.Name;
            existedWorker.Description = serverWorker.Description;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return serverWorker;
        }

        public async Task<bool> DeleteAsync(Guid workerId, CancellationToken cancellationToken)
        {
            var existedWorker = await _context.ServerWorkers.FindAsync(workerId, cancellationToken);
            if (existedWorker == null)
            {
                return false;
            }

            _context.ServerWorkers.Remove(existedWorker);

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
