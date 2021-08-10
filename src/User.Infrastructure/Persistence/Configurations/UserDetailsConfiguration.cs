using User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Todolist.Infrastructure.Persistence.Configurations
{
    public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Shoesize)
                .IsRequired();
        }
    }
}