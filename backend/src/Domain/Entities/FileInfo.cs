using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FileInfo : Entity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string PublicUrl { get; set; }
        public string ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public VacancyCandidate VacancyCandidate { get; set; }
    }
}