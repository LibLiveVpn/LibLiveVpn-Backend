using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Domain.Models;
using LibLiveVpn_Backend.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibLiveVpn_Backend.Persistence.Repositories
{
    public class WorkerCommandTaskRepository : IWorkerCommandTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkerCommandTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkerCommandTask>> GetAsync(CancellationToken cancellationToken)
        {
            return await _context.WorkerCommands.AsNoTracking()
                .Select(e => new WorkerCommandTask
                {
                    Id = e.Id,
                    InProccess = e.InProccess,
                    CommandName = e.CommandName,
                    InterfaceName = e.InterfaceName,
                    StatusCode = e.StatusCode,
                    Detail = e.Detail
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkerCommandTask?> GetByIdAsync(Guid workerId, CancellationToken cancellationToken)
        {
            return await _context.WorkerCommands.AsNoTracking()
                .Select(e => new WorkerCommandTask
                {
                    Id = e.Id,
                    InProccess = e.InProccess,
                    CommandName = e.CommandName,
                    InterfaceName = e.InterfaceName,
                    StatusCode = e.StatusCode,
                    Detail = e.Detail
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<WorkerCommandTask?> CreateAsync(WorkerCommandTask workerCommandTask, CancellationToken cancellationToken)
        {
            workerCommandTask.Id = Guid.NewGuid();

            var workerCommandTaskEntity = new WorkerCommandTaskEntity
            {
                Id = workerCommandTask.Id,
                CommandName = workerCommandTask.CommandName,
                InterfaceName = workerCommandTask.InterfaceName
            };

            await _context.WorkerCommands.AddAsync(workerCommandTaskEntity, cancellationToken);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return workerCommandTask;
        }

        public async Task<WorkerCommandTask?> UpdateAsync(WorkerCommandTask workerCommandTask, CancellationToken cancellationToken)
        {
            var existedWorkerCommandTask = await _context.WorkerCommands.FindAsync(workerCommandTask.Id, cancellationToken);
            if (existedWorkerCommandTask == null)
            {
                return null;
            }

            existedWorkerCommandTask.InProccess = workerCommandTask.InProccess;
            existedWorkerCommandTask.StatusCode = workerCommandTask.StatusCode;
            existedWorkerCommandTask.Detail = workerCommandTask.Detail;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return null;
            }

            return workerCommandTask;
        }
    }
}
