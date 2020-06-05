using Elect.DI.Attributes;
using Goblin.Landing.Contract.Repository.Interfaces;
using Goblin.Landing.Contract.Repository.Models;

namespace Goblin.Landing.Repository
{
    [ScopedDependency(ServiceType = typeof(IRepository<>))]
    public class Repository<T> : Elect.Data.EF.Services.Repository.Repository<T>, IRepository<T> where T : GoblinEntity, new()
    {
        public Repository(Elect.Data.EF.Interfaces.DbContext.IDbContext dbContext) : base(dbContext)
        {
        }
    }
}