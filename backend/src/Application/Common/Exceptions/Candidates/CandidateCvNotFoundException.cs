using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions.Candidates
{
    public class CandidateCvNotFoundException : NotFoundException
    {
        public CandidateCvNotFoundException(string candidateId) : base($"CV for candidate with id {candidateId} is not found")
        {
        }
    }
}
