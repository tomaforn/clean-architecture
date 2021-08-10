using Common.Infrastructure.Identity;
using Common.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.User.Application.Interfaces;
using Modules.User.Application.Shared.Interfaces;
using Shared.Application.Interfaces;
using System.Reflection;
using User.Domain.Entities;

namespace Modules.User.Infrastructure.Persistence
{
    public class UserDbContext : DbContextBase, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options,
               ICurrentUserService currentUserService,
               IDomainEventService domainEventService,
               IDateTime dateTime) : base(options, currentUserService, domainEventService, dateTime) { }
        public DbSet<UserDetails> UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("User");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}
