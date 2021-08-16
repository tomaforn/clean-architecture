using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Ticket.Infrastructure.Persistence.Configurations
{
    public class WorkorderConfiguration : IEntityTypeConfiguration<Domain.Entities.Workorder>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Workorder> builder)
        {
            builder.Ignore(e => e.DomainEvents);
        }
    }
}