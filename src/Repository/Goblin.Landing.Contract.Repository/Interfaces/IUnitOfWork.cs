using Goblin.Landing.Contract.Repository.Models;

namespace Goblin.Landing.Contract.Repository.Interfaces
{
    public interface IUnitOfWork : Elect.Data.EF.Interfaces.UnitOfWork.IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : GoblinEntity, new();
    }
}