using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Ticket.Application.Commands;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class TicketController : ApiControllerBase
    {   
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTicketCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
