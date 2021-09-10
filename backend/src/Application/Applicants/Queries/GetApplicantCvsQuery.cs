using Application.Common.Files.Dtos;
using Domain.Interfaces.Read;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Applicants.Queries
{
    public class GetApplicantCvsQuery : IRequest<IEnumerable<ApplicantCvDto>>
    {
        public string ApplicantId { get; }

        public GetApplicantCvsQuery(string applicantID)
        {
            ApplicantId = applicantID;
        }
    }

    public class GetApplicantCvsQueryHandler : IRequestHandler<GetApplicantCvsQuery, IEnumerable<ApplicantCvDto>>
    {
        private readonly IApplicantCvFileReadRepository _applicantCvFileReadRepository;

        public GetApplicantCvsQueryHandler(IApplicantCvFileReadRepository applicantCvFileReadRepository)
        {
            _applicantCvFileReadRepository = applicantCvFileReadRepository;
        }

        public async Task<IEnumerable<ApplicantCvDto>> Handle(GetApplicantCvsQuery request, CancellationToken cancellationToken)
        {
            var resultList = new List<ApplicantCvDto>();
            var idUrlNameTuples = await _applicantCvFileReadRepository.GetSignedUrlsAsync(request.ApplicantId);

            foreach (var idNameUrlTuple in idUrlNameTuples)
            {
                resultList.Add(new ApplicantCvDto(idNameUrlTuple.id, idNameUrlTuple.url, idNameUrlTuple.name));
            }

            return resultList;
        }
    }
}
