using Microsoft.EntityFrameworkCore;
using Modules.User.Application.Shared.Interfaces;
using Modules.Ticket.Application.Interfaces;
using Shared.Application.Interfaces;
using Shared.Infrastructure.Persistence;
using System.Reflection;

namespace Modules.Ticket.Infrastructure.Persistence
{
    public class TicketDbContext : DbContextBase, ITicketDbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options,
                ICurrentUserService currentUserService,
                IDomainEventService domainEventService,
                IDateTime dateTime) : base(options, currentUserService, domainEventService, dateTime){ }

        public DbSet<Domain.Entities.Ticket> Tickets { get; set; }
        public DbSet<Domain.Entities.Workorder> Workorders { get; set; }
        public DbSet<Domain.Entities.Equipment> Equipments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Ticket");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}