﻿using Elect.Data.EF.Interfaces.DbContext;
using Elect.DI.Attributes;
using Microsoft.EntityFrameworkCore;
using Goblin.Landing.Contract.Repository.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Goblin.Landing.Repository
{
    [ScopedDependency(ServiceType = typeof(IBootstrapper))]
    public class Bootstrapper : IBootstrapper
    {
        private readonly IDbContext _dbContext;

        public Bootstrapper(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task InitialAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task RebuildAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}