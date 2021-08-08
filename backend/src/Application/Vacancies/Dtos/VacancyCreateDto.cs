using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Stages.Dtos;
using Application.Tags.Dtos;
using Domain.Enums;

namespace Application.Vacancies.Dtos
{
    public class VacancyCreateDto : Dto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string ProjectId { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public Tier TierFrom { get; set; }
        public Tier TierTo { get; set; }
        public string Sources { get; set; }
        public bool IsHot { get; set; }
        public bool IsRemote { get; set; }
        //public ICollection<TagDto> Tags { get; set; }
        //public ICollection<StageWithCandidatesDto> Stages { get; set; }
        public string CompanyId { get; set; }
        public string ResponsibleHrId { get; set; }
    }
}
