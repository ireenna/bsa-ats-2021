using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Commands;
using Application.Vacancies.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class VacancyController : ApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateVacancy(VacancyCreateDto vacancy)
        {
            var command = new CreateEntityCommand<VacancyCreateDto>(vacancy);
            return StatusCode(201, await Mediator.Send(command));
        }
    }
}
