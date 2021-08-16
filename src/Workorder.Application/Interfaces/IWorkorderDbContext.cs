using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.Interfaces
{
    public interface IWorkorderDbContext
    {
        DbSet<Domain.Entities.Workorder> Workorders { get; set; }

        DbSet<Domain.Entities.Activity> Activities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
