using Microsoft.EntityFrameworkCore;
using Modules.User.Application.Shared.Interfaces;
using Modules.Equipment.Application.Interfaces;
using Shared.Application.Interfaces;
using Shared.Infrastructure.Persistence;
using System.Reflection;

namespace Modules.Equipment.Infrastructure.Persistence
{
    public class EquipmentDbContext : DbContextBase, IEquipmentDbContext
    {
        public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options,
                ICurrentUserService currentUserService,
                IDomainEventService domainEventService,
                IDateTime dateTime) : base(options, currentUserService, domainEventService, dateTime){ }

        public DbSet<Domain.Entities.Equipment> Equipment { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Equipment");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}