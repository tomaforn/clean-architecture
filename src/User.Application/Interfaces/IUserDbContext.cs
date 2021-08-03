using System.Threading;
using System.Threading.Tasks;

namespace Modules.User.Application.Interfaces
{
    public interface IUserDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
