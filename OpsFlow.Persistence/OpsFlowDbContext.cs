using Microsoft.EntityFrameworkCore;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Persistence
{
    public class OpsFlowDbContext : DbContext
    {
        public OpsFlowDbContext(DbContextOptions<OpsFlowDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Workflow> Workflows { get; set; }
    }
}
