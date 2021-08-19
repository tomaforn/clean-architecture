using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Equipment.Application.Commands;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class EquipmentController : ApiControllerBase
    {   
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateEquipmentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new ScrapEquipmentCommand { Id = id });
            return NoContent();
        }
    }
}
