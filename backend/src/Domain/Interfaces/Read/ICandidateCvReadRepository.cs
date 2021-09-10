using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Read
{
    public interface ICandidateCvReadRepository
    {
        Task<string> GetSignedUrlAsync(string candidateId);
    }
}
