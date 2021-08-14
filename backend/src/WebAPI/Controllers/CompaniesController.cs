using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Stages.Queries;

namespace WebAPI.Controllers
{
    public class CompaniesController : ApiController
    {
        [HttpGet("by-vacancy/{id}")]
        public async Task<IActionResult> GetByVacancyAsyncId([FromRoute] string id)
        {
            var query = new GetStagesByVacancyQuery(id);
            return Ok(await Mediator.Send(query));
        }
    }
}
