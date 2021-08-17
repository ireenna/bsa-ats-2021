using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stages.Dtos
{
    public class StageCreateDto
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int Index { get; set; }
        public bool IsReviewable { get; set; }
        public string VacancyId { get; set; }

    }

    public class StageCreateDtoValidator : AbstractValidator<StageCreateDto>
    {
        public StageCreateDtoValidator()
        {
            RuleFor(_ => _.Name).NotNull().NotEmpty();
            RuleFor(_ => _.Type).GreaterThanOrEqualTo(0).LessThan((int)StageType.Hired);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
            RuleFor(_ => _.VacancyId).NotNull().NotEmpty();
        }
    }

}
