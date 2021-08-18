using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Queries;
using Application.MailTemplates.Dtos;
using Microsoft.AspNetCore.Authorization;
using Application.Common.Commands;
using Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MailTemplatesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<MailTemplateDto>> GetMailTempaleList()
        {
            var query = new GetEntityListQuery<MailTemplateDto>();
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MailTemplateDto>> GetMailTempale(string id)
        {
            var query = new GetEntityByIdQuery<MailTemplateDto>(id);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<MailTemplateDto>> CreateMailTempale([FromBody] MailTemplateDto mailTemplateDto)
        {
            var query = new CreateEntityCommand<MailTemplateDto>(mailTemplateDto);
            return Ok(await Mediator.Send(query));
        }

        [HttpPut]
        public async Task<ActionResult<MailTemplateDto>> UpdateMailTempale([FromBody] MailTemplateDto mailTemplateDto)
        {
            var query = new UpdateEntityCommand<MailTemplateDto>(mailTemplateDto);
            return Ok(await Mediator.Send(query));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMailTempale(string id)
        {
            var query = new DeleteEntityCommand(id);
            return Ok(await Mediator.Send(query));
        }
    }
}
