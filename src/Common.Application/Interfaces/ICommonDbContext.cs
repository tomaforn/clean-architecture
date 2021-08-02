using System.Threading;
using System.Threading.Tasks;

namespace Modules.Todolist.Application.Interfaces
{
    public interface ICommonDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
