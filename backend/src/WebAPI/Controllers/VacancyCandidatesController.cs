using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.VacancyCandidates.Dtos;
using Application.VacancyCandidates.Queries;
using Application.VacancyCandidates.Commands;
using Microsoft.AspNetCore.Http;
using Application.Common.Files.Dtos;
using System;

namespace WebAPI.Controllers
{
    public class VacancyCandidates : ApiController
    {
        [HttpGet("{vacancyId}/{id}/full")]
        public async Task<ActionResult<VacancyCandidateFullDto>> GetFull(
            [FromRoute] string id,
            [FromRoute] string vacancyId
        )
        {
            var query = new GetFullVacancyCandidateByIdQuery(id, vacancyId);
            return Ok(await Mediator.Send(query));
        }

        [HttpPut("{id}/set-stage/{vacancyId}/{stageId}")]
        public async Task<ActionResult<VacancyCandidateDto>> ChangeCandidateStage(
            [FromRoute] string id,
            [FromRoute] string vacancyId,
            [FromRoute] string stageId
        )
        {
            var command = new ChangeCandidateStageCommand(id, vacancyId, stageId);
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CandidatesRange/{vacancyId}")]
        public async Task<IActionResult> PostRangeOfCandidatesAsync(string[] applicantsIds, string vacancyId)
        {
            var command = new CreateVacancyCandidateRangeCommand(applicantsIds, vacancyId);

            return Ok(await Mediator.Send(command));
        }
        [HttpPost("{vacancyId}/{id}")]
        public async Task<IActionResult> PostVacancyCandidateNoAuth(string id, string vacancyId, [FromBody] IFormFile cvFile)
        {
            var cvFileDto = new FileDto(cvFile.OpenReadStream(), cvFile.FileName);
            var command = new CreateVacancyCandidateNoAuthCommand(id, vacancyId, cvFileDto);

            return Ok(await Mediator.Send(command));
        }
        [HttpPost("viewed/{id}")]
        public async Task<IActionResult> PostMarkAsViewed(string id)
        {
            var command = new MarkAsViewedCommand(id);

            return Ok(await Mediator.Send(command));
        }
        [HttpPost("{id}/cv")]
        public async Task<IActionResult> PostCandidateCv(string id, [FromForm] IFormFile? cvFile)
        {
            var cvFileDto = cvFile != null ? new FileDto(cvFile.OpenReadStream(), cvFile.FileName) : null;
            var command = new UploadCvForCandidateCommand(id, cvFileDto);

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}/cv")]
        public async Task<IActionResult> DeleteCandidateCv(string id)
        {
            var command = new DeleteCandidateCvCommand(id);
            return Ok(await Mediator.Send(command));
        }
    }
}
