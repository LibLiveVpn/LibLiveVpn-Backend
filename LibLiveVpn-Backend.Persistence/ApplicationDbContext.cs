﻿using LibLiveVpn_Backend.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibLiveVpn_Backend.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<ServerEntity> Servers { get; set; }

        public DbSet<ServerWorkerEntity> ServerWorkers { get; set; }

        public DbSet<WorkerCommandTaskEntity> WorkerCommands { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
