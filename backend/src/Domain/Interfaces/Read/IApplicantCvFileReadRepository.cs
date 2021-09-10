using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Read
{
    public interface IApplicantCvFileReadRepository
    {
        Task<IEnumerable<(string id, string name, string url)>> GetSignedUrlsAsync(string applicantId);
    }
}