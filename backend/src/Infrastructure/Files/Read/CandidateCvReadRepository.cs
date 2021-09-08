using Domain.Interfaces.Read;
using Infrastructure.Files.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Files.Read
{
    public class CandidateCvReadRepository : ICandidateCvReadRepository
    {
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IVacancyCandidateReadRepository _candidateReadRepository;

        public CandidateCvReadRepository(IFileReadRepository fileReadRepository, IVacancyCandidateReadRepository candidateReadRepository)
        {
            _fileReadRepository = fileReadRepository;
            _candidateReadRepository = candidateReadRepository;
        }

        public async Task<string> GetSignedUrlAsync(string candidateId)
        {
            var candidateCvFileInfo = await _candidateReadRepository.GetCvFileInfoAsync(candidateId);

            return await _fileReadRepository.GetSignedUrlAsync(
                candidateCvFileInfo.Path,
                candidateCvFileInfo.Name,
                TimeSpan.FromMinutes(5));
        }
    }
}
