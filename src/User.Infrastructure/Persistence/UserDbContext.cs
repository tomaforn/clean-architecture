using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Identity;
using System.Reflection;
using User.Domain.Entities;

namespace Modules.User.Infrastructure.Persistence
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDetails> UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("User");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}
