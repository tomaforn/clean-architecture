using Microsoft.AspNetCore.Identity;
using Modules.User.Application.Shared.Models;
using System.Linq;

namespace Models.User.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}