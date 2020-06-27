using Goblin.Landing.Contract.Repository.Interfaces;

namespace Goblin.Landing.Service.Base
{
    public abstract class Service
    {
        protected readonly IGoblinUnitOfWork GoblinUnitOfWork;

        protected Service(IGoblinUnitOfWork goblinUnitOfWork)
        {
            GoblinUnitOfWork = goblinUnitOfWork;
        }
    }
}