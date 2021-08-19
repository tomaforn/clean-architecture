using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Ticket.Infrastructure.Persistence.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Equipment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Equipment> builder)
        {
            builder.Ignore(e => e.DomainEvents);
        }
    }
}