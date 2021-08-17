using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stages.Dtos
{
    public class StageDto
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int Index { get; set; }
        public bool IsReviewable { get; set; }
        public string VacancyId { get; set; }
    }
}
