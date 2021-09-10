using MediatR;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Applicants.Dtos;
using Domain.Interfaces.Abstractions;
using Domain.Interfaces.Read;
using System;
using Application.Common.Queries;
using Application.ElasticEnities.Dtos;
using AutoMapper;

namespace Application.Applicants.Queries
{
    public class GetComposedApplicantListQuery : IRequest<IEnumerable<ApplicantDto>>
    { }

    public class GetComposedApplicantListQueryHandler : IRequestHandler<GetComposedApplicantListQuery, IEnumerable<ApplicantDto>>
    {
        private readonly IApplicantReadRepository _applicantReadRepository;
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        
        public GetComposedApplicantListQueryHandler(
            IApplicantReadRepository applicantRepository,
            ISender mediator, 
            IMapper mapper)
        {
            _mediator = mediator;
            _applicantReadRepository = applicantRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicantDto>> Handle(GetComposedApplicantListQuery query, CancellationToken _)
        {
            var applicantList = await _applicantReadRepository.GetCompanyApplicants();
            var applicantResultList = new List<ApplicantDto>();

            foreach (var applicant in applicantList)
            {
                var tagsQueryTask = _mediator.Send(new GetElasticDocumentByIdQuery<ElasticEnitityDto>(applicant.Id));

                var applicantDto = _mapper.Map<ApplicantDto>(applicant);

                applicantDto.Tags = await tagsQueryTask;
                applicantDto.Vacancies = _mapper.Map<IEnumerable<ApplicantVacancyInfoDto>>(await _applicantReadRepository.GetApplicantVacancyInfoListAsync(applicant.Id));

                applicantResultList.Add(applicantDto);
            }

            return applicantResultList;
        }
    }
}