using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Projects.Dtos
{
    public class ProjectDto : Dto
    {
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeamInfo { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string WebsiteLink { get; set; }
        public string CompanyId { get; set; }
    }
}
