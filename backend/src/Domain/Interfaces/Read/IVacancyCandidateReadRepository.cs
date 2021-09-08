using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Abstractions;

namespace Domain.Interfaces.Read
{
    public interface IVacancyCandidateReadRepository : IReadRepository<VacancyCandidate>
    {
        Task<FileInfo> GetCvFileInfoAsync(string candidateId);
        Task<VacancyCandidate> GetFullAsync(string id, string vacancyId);
        Task<VacancyCandidate> GetFullByApplicantAndStageAsync(string applicantId, string stageId);
    }
}
