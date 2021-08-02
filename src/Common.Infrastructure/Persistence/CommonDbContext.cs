using Common.Application.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Modules.Todolist.Application.Interfaces;
using System.Reflection;

namespace Common.Infrastructure.Persistence
{
    public class CommonDbContext : DbContextBase, ICommonDbContext
    {
        public CommonDbContext(
            DbContextOptions<CommonDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options, operationalStoreOptions, currentUserService, domainEventService, dateTime) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
