using Domain.Interfaces.Read;
using Domain.Interfaces.Write;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.VacancyCandidates.Commands
{
    public class DeleteCandidateCvCommand : IRequest<Unit>
    {
        public string Id { get; set; }

        public DeleteCandidateCvCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteCandidateCvCommandHandler : IRequestHandler<DeleteCandidateCvCommand, Unit>
    {
        private readonly ICandidateCvWriteRepository _candidateCvWriteRepository;
        private readonly IVacancyCandidateWriteRepository _candidateWriteRepository;
        private readonly IVacancyCandidateReadRepository _candidateReadRepository;
        public DeleteCandidateCvCommandHandler(
            IVacancyCandidateWriteRepository candidateWriteRepository,
            IVacancyCandidateReadRepository candidateReadRepository,
            ICandidateCvWriteRepository candidateCvWriteRepository)
        {
            _candidateCvWriteRepository = candidateCvWriteRepository;
            _candidateReadRepository = candidateReadRepository;
            _candidateWriteRepository = candidateWriteRepository;
        }
        public async Task<Unit> Handle(DeleteCandidateCvCommand command, CancellationToken _)
        {
            var fileInfo = await _candidateReadRepository.GetCvFileInfoAsync(command.Id);
            
            var candidate = await _candidateReadRepository.GetAsync(command.Id);
            candidate.CvFileInfoId = null;
            await _candidateWriteRepository.UpdateAsync(candidate);

            await _candidateCvWriteRepository.DeleteAsync(fileInfo);
            
            return Unit.Value;
        }
    }
}
