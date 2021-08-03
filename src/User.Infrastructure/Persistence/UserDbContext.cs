using Common.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.User.Application.Interfaces;
using System.Reflection;

namespace Models.User.Infrastructure.Persistence
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("User");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}
