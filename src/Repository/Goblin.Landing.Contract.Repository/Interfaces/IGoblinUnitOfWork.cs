using Goblin.Landing.Contract.Repository.Models;

namespace Goblin.Landing.Contract.Repository.Interfaces
{
    public interface IGoblinUnitOfWork : Elect.Data.EF.Interfaces.UnitOfWork.IUnitOfWork
    {
        IGoblinRepository<T> GetRepository<T>() where T : GoblinEntity, new();
    }
}