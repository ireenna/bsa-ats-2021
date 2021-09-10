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
    public class AttachCvForCandidateCommand : IRequest<CvFileInfoDto>
    {
        public string Id { get; set; }
        public string CvFileId { get; set; }

        public AttachCvForCandidateCommand(string id, string cvFileId)
        {
            Id = id;
            CvFileId = cvFileId;
        }
    }

    public class UploadCvForCandidateCommandHandler : IRequestHandler<AttachCvForCandidateCommand, CvFileInfoDto>
    {
        private readonly IVacancyCandidateWriteRepository _vacancyCandidateWriteRepository;
        private readonly IVacancyCandidateReadRepository _vacancyCandidateReadRepository;
        private readonly IApplicantReadRepository _applicantReadRepository;
        private readonly ICandidateCvReadRepository _candidateCvReadRepository;
        private readonly IMapper _mapper;
        public UploadCvForCandidateCommandHandler(
            IVacancyCandidateReadRepository vacancyCandidateReadRepository,
            IVacancyCandidateWriteRepository vacancyCandidateWriteRepository,
            ICandidateCvReadRepository candidateCvReadRepository,
            IApplicantReadRepository applicantReadRepository,
            IMapper mapper)
        {
            _vacancyCandidateWriteRepository = vacancyCandidateWriteRepository;
            _vacancyCandidateReadRepository = vacancyCandidateReadRepository;
            _candidateCvReadRepository = candidateCvReadRepository;
            _applicantReadRepository = applicantReadRepository;
            _mapper = mapper;
        }
        public async Task<CvFileInfoDto> Handle(AttachCvForCandidateCommand command, CancellationToken _)
        {
            var candidate = await _vacancyCandidateReadRepository.GetAsync(command.Id);
            candidate.CvFileInfoId = command.CvFileId;

            await _vacancyCandidateWriteRepository.UpdateAsync(candidate);

            var cvFileInfo = (await _applicantReadRepository.GetCvFileInfosAsync(candidate.ApplicantId))
                .First(f => f.Id == command.CvFileId);
            cvFileInfo.PublicUrl = await _candidateCvReadRepository.GetSignedUrlAsync(candidate.Id);
            cvFileInfo.Name = cvFileInfo.Name.Split('-')[5];
            
            return _mapper.Map<CvFileInfoDto>(cvFileInfo);
        }
    }
}
