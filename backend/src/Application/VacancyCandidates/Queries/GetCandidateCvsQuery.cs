using Application.Common.Files.Dtos;
using AutoMapper;
using Domain.Interfaces.Read;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.VacancyCandidates.Queries
{
    public class GetCandidateCvsQuery : IRequest<IEnumerable<CvFileInfoDto>>
    {
        public string Id { get; set; }
        public GetCandidateCvsQuery(string id)
        {
            Id = id;
        }
    }

    public class GetCandidateCvsQueryHandler : IRequestHandler<GetCandidateCvsQuery, IEnumerable<CvFileInfoDto>>
    {
        private readonly IVacancyCandidateReadRepository _candidateReadRepository;
        private readonly IApplicantReadRepository _applicantReadRepository;
        private readonly IMapper _mapper;
        public GetCandidateCvsQueryHandler(
            IVacancyCandidateReadRepository candidateReadRepository,
            IApplicantReadRepository applicantReadRepository,
            IMapper mapper)
        {
            _candidateReadRepository = candidateReadRepository;
            _applicantReadRepository = applicantReadRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CvFileInfoDto>> Handle(GetCandidateCvsQuery query, CancellationToken _)
        {
            var candidate = await _candidateReadRepository.GetAsync(query.Id);
            var fileInfos = (await _applicantReadRepository.GetCvFileInfosAsync(candidate.ApplicantId)).ToArray();

            for (int i = 0; i < fileInfos.Length; i++)
            {
                fileInfos[i].Name = fileInfos[i].Name.Split('-')[5];
            }

            return _mapper.Map<IEnumerable<CvFileInfoDto>>(fileInfos);
        }
    }
}
