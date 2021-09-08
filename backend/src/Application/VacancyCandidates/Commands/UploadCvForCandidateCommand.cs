using Application.Common.Files.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Domain.Entities;
using System.Threading.Tasks;
using Domain.Interfaces.Write;
using AutoMapper;
using Domain.Interfaces.Read;

namespace Application.VacancyCandidates.Commands
{
    public class UploadCvForCandidateCommand : IRequest<CvFileInfoDto>
    {
        public string Id { get; set; }
        public FileDto? CvFile { get; set; }

        public UploadCvForCandidateCommand(string id, FileDto? cvFile)
        {
            Id = id;
            CvFile = cvFile;
        }
    }

    public class UploadCvForCandidateCommandHandler : IRequestHandler<UploadCvForCandidateCommand, CvFileInfoDto>
    {
        private readonly ICandidateCvWriteRepository _candidateCvWriteRepository;
        private readonly ICandidateCvReadRepository _candidateCvReadRepository;
        private readonly IVacancyCandidateReadRepository _vacancyCandidateReadRepository;
        private readonly IMapper _mapper;
        public UploadCvForCandidateCommandHandler(
            ICandidateCvWriteRepository candidateCvWriteRepository,
            IVacancyCandidateReadRepository vacancyCandidateReadRepository,
            ICandidateCvReadRepository candidateCvReadRepository,
            IMapper mapper)
        {
            _candidateCvWriteRepository = candidateCvWriteRepository;
            _candidateCvReadRepository = candidateCvReadRepository;
            _vacancyCandidateReadRepository = vacancyCandidateReadRepository;
            _mapper = mapper;
        }
        public async Task<CvFileInfoDto> Handle(UploadCvForCandidateCommand command, CancellationToken _)
        {
            var candidateCv = await _vacancyCandidateReadRepository.GetCvFileInfoAsync(command.Id);

            if (candidateCv == null)
            {
                var fileInfo = await _candidateCvWriteRepository.UploadAsync(command.Id, command.CvFile.Content);
                fileInfo.PublicUrl = await _candidateCvReadRepository.GetSignedUrlAsync(command.Id);

                return _mapper.Map<CvFileInfoDto>(fileInfo);
            }
            else
            {
                await _candidateCvWriteRepository.UpdateAsync(command.Id, command.CvFile.Content);

                return new CvFileInfoDto()
                {
                    Name = command.CvFile.FileName,
                    Url = await _candidateCvReadRepository.GetSignedUrlAsync(command.Id)
                };
            }
        }
    }
}
