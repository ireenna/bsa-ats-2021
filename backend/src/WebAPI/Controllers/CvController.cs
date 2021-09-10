using Application.Applicants.Commands;
using Application.Applicants.Commands.CreateApplicant;
using Application.Applicants.Commands.DeleteApplicant;
using Application.Applicants.Dtos;
using Application.Applicants.Queries;
using Application.Common.Commands;
using Application.Common.Files.Dtos;
using Application.Common.Queries;
using Application.Cv.Commands;
using Application.ElasticEnities.CommandQuery.AddTagCommand;
using Application.ElasticEnities.CommandQuery.DeleteTagCommand;
using Application.ElasticEnities.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class CvController : ApiController
    {
        protected IMapper _mapper;
        public CvController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpDelete("{id}/applicant/{applicantId}")]
        public async Task<IActionResult> DeleteCv([FromRoute] string id, [FromRoute] string applicantId)
        {
            var command = new DeleteCvCommand(id, applicantId);
            return Ok(await Mediator.Send(command));
        }
    }
}