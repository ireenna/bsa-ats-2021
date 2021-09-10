using Domain.Interfaces.Read;
using Domain.Interfaces.Write;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cv.Commands
{
    public class DeleteCvCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string ApplicantId { get; set; }

        public DeleteCvCommand(string id, string applicantId)
        {
            Id = id;
            ApplicantId = applicantId;
        }

        public class DeleteCvCommandHandler : IRequestHandler<DeleteCvCommand, Unit>
        {
            private readonly IApplicantCvFileWriteRepository _applicantCvFileWriteRepository;
            private readonly IApplicantReadRepository _applicantReadRepository;
            public DeleteCvCommandHandler(
                IApplicantCvFileWriteRepository applicantCvFileWriteRepository,
                IApplicantReadRepository applicantReadRepository)
            {
                _applicantCvFileWriteRepository = applicantCvFileWriteRepository;
                _applicantReadRepository = applicantReadRepository;
            }

            public async Task<Unit> Handle(DeleteCvCommand command, CancellationToken _)
            {
                var fileInfo = (await _applicantReadRepository.GetCvFileInfosAsync(command.ApplicantId))
                    .FirstOrDefault(f => f.Id == command.Id);

                await _applicantCvFileWriteRepository.DeleteAsync(fileInfo);

                return Unit.Value;
            }
        }
    }
}
