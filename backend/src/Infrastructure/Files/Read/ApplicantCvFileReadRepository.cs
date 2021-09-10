using Domain.Interfaces.Read;
using Infrastructure.Files.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Files.Read
{
    public class ApplicantCvFileReadRepository : IApplicantCvFileReadRepository
    {
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IApplicantReadRepository _applicantReadRepository;

        public ApplicantCvFileReadRepository(IFileReadRepository fileReadRepository, IApplicantReadRepository applicantReadRepository)
        {
            _fileReadRepository = fileReadRepository;
            _applicantReadRepository = applicantReadRepository;
        }

        public async Task<IEnumerable<(string id, string name, string url)>> GetSignedUrlsAsync(string applicantId)
        {
            var urls = new List<(string id, string name, string url)>();
            var applicantCvFileInfos = await _applicantReadRepository.GetCvFileInfosAsync(applicantId);

            foreach (var fileInfo in applicantCvFileInfos)
            {
                var fileNamePart = fileInfo.Name.Split('-')[5];
                urls.Add((fileInfo.Id, fileNamePart, await _fileReadRepository.GetSignedUrlAsync(
                    fileInfo.Path,
                    fileInfo.Name,
                    TimeSpan.FromMinutes(5)))
                );
            }

            return urls;
        }
    }
}
