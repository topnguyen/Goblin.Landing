using Goblin.Landing.Contract.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Goblin.Landing.Repository
{
    public sealed partial class GoblinDbContext
    {
        public DbSet<SampleEntity> Samples { get; set; }
    }
}