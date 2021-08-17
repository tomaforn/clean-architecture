using BlazorUI.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Pages
{
    public class _HostAuthModel : PageModel
    {
        public readonly BlazorServerAuthStateCache Cache;
        public _HostAuthModel(BlazorServerAuthStateCache cache)
        {
            Cache = cache;
        }
        public async Task<IActionResult> OnGet()
        {

            if (User.Identity.IsAuthenticated)
            {
                var sid = User.Claims
                    .Where(c => c.Type.Equals("sid"))
                    .Select(c => c.Value)
                    .FirstOrDefault();

                if (sid != null && !Cache.HasSubjectId(sid))
                {
                    var authResult = await HttpContext.AuthenticateAsync("oidc");
                    DateTimeOffset expiration = authResult.Properties.ExpiresUtc.Value;
                    string accessToken = await HttpContext.GetTokenAsync("access_token");
                    string refreshToken = await HttpContext.GetTokenAsync("refresh_token");
                    string idToken = await HttpContext.GetTokenAsync("id_token");
                    string expiresAtToken = await HttpContext.GetTokenAsync("expires_at");
                    if (!DateTimeOffset.TryParse(expiresAtToken, out var refreshAt)
                        || refreshAt < DateTimeOffset.UtcNow
                        || string.IsNullOrWhiteSpace(refreshToken))
                    {
                        refreshAt = DateTimeOffset.MaxValue;
                    }
                    else
                    {
                        var seconds = refreshAt.Subtract(DateTimeOffset.UtcNow).TotalSeconds;
                        refreshAt = DateTimeOffset.UtcNow.AddSeconds(seconds / 2);
                    }
                    Cache.Add(sid, expiration, idToken, accessToken, refreshToken, refreshAt);
                }
            }
            return Page();
        }

        public IActionResult OnGetLogin()
        {
            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(15),
                RedirectUri = Url.Content("~/")
            };
            return Challenge(authProps, "oidc");
        }

        public async Task OnGetLogout()
        {
            var authProps = new AuthenticationProperties
            {
                RedirectUri = Url.Content("~/")
            };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc", authProps);
        }
    }
}
