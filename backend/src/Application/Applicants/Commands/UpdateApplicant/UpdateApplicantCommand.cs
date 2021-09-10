using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Applicants.Dtos;
using Application.Common.Commands;
using Application.ElasticEnities.Dtos;
using Application.Common.Files.Dtos;
using Domain.Interfaces.Read;
using Domain.Interfaces.Abstractions;
using Domain.Entities;
using System.Linq;
using Domain.Interfaces.Write;

#nullable enable

namespace Application.Applicants.Commands
{
    public class UpdateApplicantCommand : IRequest<ApplicantDto>
    {
        public UpdateApplicantDto Entity { get; set; }
        public FileDto[] CvFileDtos { get; set; }
        public FileDto? PhotoFileDto { get; set; }

        public UpdateApplicantCommand(UpdateApplicantDto entity, FileDto[] cvFileDtos, FileDto? photoFileDto)
        {
            Entity = entity;
            CvFileDtos = cvFileDtos;
            PhotoFileDto = photoFileDto;
        }
    }

    public class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand, ApplicantDto>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly IApplicantReadRepository _repository;
        private readonly IWriteRepository<Applicant> _writeRepository;
        private readonly IApplicantCvFileWriteRepository _applicantCvFileWriteRepository;
        private readonly IWriteRepository<FileInfo> _fileInfoWriteRepository;

        public UpdateApplicantCommandHandler(
            IApplicantReadRepository repository,
            IWriteRepository<Applicant> writeRepository, 
            IApplicantCvFileWriteRepository applicantCvFileWriteRepository,
            IWriteRepository<FileInfo> fileInfoWriteRepository,
            ISender mediator,
            IMapper mapper)
        {
            _repository = repository;
            _writeRepository = writeRepository;
            _applicantCvFileWriteRepository = applicantCvFileWriteRepository;
            _fileInfoWriteRepository = fileInfoWriteRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ApplicantDto> Handle(UpdateApplicantCommand command, CancellationToken _)
        {
            var updatableApplicant = _mapper.Map<Applicant>(command.Entity);
            updatableApplicant.CvFileInfos = (await _repository.GetCvFileInfosAsync(command.Entity.Id)).ToList();
            await UploadCvFileIfExists(command, updatableApplicant);
            var updatedApplicantEntity = await _writeRepository.UpdateAsync(updatableApplicant);

            var updatedApplicant = _mapper.Map<ApplicantDto>(updatedApplicantEntity);

            var elasticQuery = new UpdateElasticDocumentCommand<UpdateApplicantToTagsDto>(
                _mapper.Map<UpdateApplicantToTagsDto>(command.Entity.Tags)
            );

            updatedApplicant.Tags = _mapper.Map<ElasticEnitityDto>(await _mediator.Send(elasticQuery));
            updatedApplicant.Vacancies = _mapper.Map<IEnumerable<ApplicantVacancyInfoDto>>
                (await _repository.GetApplicantVacancyInfoListAsync(updatedApplicant.Id));
            
            await UploadPhotoFileIfExists(command, updatableApplicant);

            return updatedApplicant;
        }

        private async Task UploadCvFileIfExists(UpdateApplicantCommand command, Applicant applicant)
        {
            if (command.CvFileDtos.Length == 0)
            {
                return;
            }

            foreach (var cvFile in command.CvFileDtos)
            {
                FileInfo uploadedCvFileInfo;

                if (cvFile.Link == null)
                {
                    uploadedCvFileInfo = await _applicantCvFileWriteRepository
                        .UploadAsync(applicant.Id, cvFile.FileName, cvFile!.Content);
                }
                else
                {
                    uploadedCvFileInfo = await _fileInfoWriteRepository
                        .CreateAsync(cvFile.ToFileInfo());
                }
                applicant.CvFileInfos.Add(uploadedCvFileInfo);
            }
        }

        private async Task UploadPhotoFileIfExists(UpdateApplicantCommand command, Applicant applicant)
        {
            if (command.PhotoFileDto == null)
            {
                return;
            }

            await _mediator.Send(new UpdateApplicantPhotoCommand(command.Entity.Id, command.PhotoFileDto!, applicant));
        }
    }
}