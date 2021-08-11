using Shared.Application.Interfaces;
using Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Modules.Todolist.Application.Interfaces;
using Modules.Todolist.Domain.Entities;
using Modules.User.Application.Shared.Interfaces;
using System.Reflection;

namespace Modules.Todolist.Infrastructure.Persistence
{
    public class TodolistDbContext : DbContextBase, ITodolistDbContext
    {
        public TodolistDbContext(DbContextOptions<TodolistDbContext> options,
                ICurrentUserService currentUserService,
                IDomainEventService domainEventService,
                IDateTime dateTime) : base(options, currentUserService, domainEventService, dateTime){ }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Todolist");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
            base.OnModelCreating(builder);
        }
    }
}
