using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Queries;
using Application.MailTemplates.Dtos;
using Microsoft.AspNetCore.Authorization;
using Application.Common.Commands;
using Domain.Entities;
using Application.MailTemplates.Commands;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Application.MailAttachments.Dtos;
using Application.MailTemplates.Queries;
using Application.Mail;

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
            var query = new GetMailTemplatesListForThisUserQuery();
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MailTemplateDto>> GetMailTempale(string id)
        {
            var d = new GetMailWithAttachmentFilesTemplateQuery(id);
            var t = await Mediator.Send(d);
            var s = new SendMailCommand("hp_94@inbox.ru", t.Subject, t.Html,"default",t.Attachments);
            return Ok(await Mediator.Send(s));
            var query = new GetEntityByIdQuery<MailTemplateDto>(id);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<MailTemplateDto>> CreateMailTempale([FromForm] string body, List<IFormFile> files)
        {
            var query = new CreateMailTemplateCommand(body, files);
            return Ok(await Mediator.Send(query));
        }

        [HttpPut]
        public async Task<ActionResult<MailTemplateDto>> UpdateMailTempale([FromForm] string body, List<IFormFile> files)
        {
            var query = new UpdateMailTemplateCommand(body, files);
            return Ok(await Mediator.Send(query));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMailTempale(string id)
        {
            var query = new DeleteMailTemplateCommand(id);
            return Ok(await Mediator.Send(query));
        }
    }
}
