using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.Interfaces
{
    public interface ITicketDbContext
    {
        DbSet<Domain.Entities.Ticket> Tickets { get; set; }
        DbSet<Domain.Entities.Workorder> TicketWorkorders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
