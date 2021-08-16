using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Workorder.Application.Commands;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class WorkorderController : ApiControllerBase
    {   
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateWorkorderCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
