using Common.Application.Interfaces;
using Common.Infrastructure.Persistence;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Modules.Todolist.Application.Interfaces;
using Modules.Todolist.Domain.Entities;
using System.Reflection;

namespace Modules.Todolist.Infrastructure.Persistence
{
    public class TodolistDbContext : DbContextBase, ITodolistDbContext
    {
        public TodolistDbContext(
            DbContextOptions<TodolistDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options, operationalStoreOptions, currentUserService, domainEventService, dateTime){}

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
