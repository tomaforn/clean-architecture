using Microsoft.EntityFrameworkCore;
using Modules.User.Application.Shared.Interfaces;
using Modules.Workorder.Application.Interfaces;
using Shared.Application.Interfaces;
using Shared.Infrastructure.Persistence;
using System.Reflection;

namespace Modules.Workorder.Infrastructure.Persistence
{
    public class WorkorderDbContext : DbContextBase, IWorkorderDbContext
    {
        public WorkorderDbContext(DbContextOptions<WorkorderDbContext> options,
                ICurrentUserService currentUserService,
                IDomainEventService domainEventService,
                IDateTime dateTime) : base(options, currentUserService, domainEventService, dateTime){ }

        public DbSet<Domain.Entities.Workorder> Workorders { get; set; }

        public DbSet<Domain.Entities.Activity> Activities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Workorder");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}