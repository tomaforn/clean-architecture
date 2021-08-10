using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        public CurrentUserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<string> GetUserId()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            return state?.User?.Claims?.ToList().Find(x => x.Type == "sub")?.Value;
        }
    }
}
