using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Services
{
    public interface ICurrentUserService
    {
        Task<string> GetUserId();
    }
}
