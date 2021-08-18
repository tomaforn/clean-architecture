using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Equipment.Application.Interfaces
{
    public interface IEquipmentDbContext
    {
        DbSet<Domain.Entities.Equipment> Equipment { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
