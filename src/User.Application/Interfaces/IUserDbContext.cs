using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace Modules.User.Application.Interfaces
{
    public interface IUserDbContext
    {
        DbSet<UserDetails> UserDetails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
