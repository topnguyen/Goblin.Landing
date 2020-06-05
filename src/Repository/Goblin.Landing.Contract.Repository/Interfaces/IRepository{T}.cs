using Goblin.Landing.Contract.Repository.Models;

namespace Goblin.Landing.Contract.Repository.Interfaces
{
    public interface IRepository<T> : Elect.Data.EF.Interfaces.Repository.IRepository<T> where T : GoblinEntity, new()
    {
    }
}