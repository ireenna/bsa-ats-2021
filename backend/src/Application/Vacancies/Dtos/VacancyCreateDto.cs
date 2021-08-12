using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Stages.Dtos;
using Application.Tags.Dtos;
using Domain.Enums;
using FluentValidation;

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
        public int TierFrom { get; set; }
        public int TierTo { get; set; }
        public string Sources { get; set; }
        public bool IsHot { get; set; }
        public bool IsRemote { get; set; }
        //public ICollection<TagDto> Tags { get; set; }
        public ICollection<StageWithCandidatesDto> Stages { get; set; }
        public string CompanyId { get; set; }
        public string ResponsibleHrId { get; set; }
    }
    public class VacancyCreateDtoValidator : AbstractValidator<VacancyCreateDto>
    {
        public VacancyCreateDtoValidator()
        {
            RuleFor(_ => _.Title).NotNull().NotEmpty();
            RuleFor(_ => _.Description).NotNull().NotEmpty();
            RuleFor(_ => _.Requirements).NotNull().NotEmpty();
            RuleFor(_ => _.ProjectId).NotNull().NotEmpty();
            RuleFor(_ => _.SalaryFrom).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(_ => _.SalaryTo).NotNull().NotEmpty().GreaterThanOrEqualTo(_=>_.SalaryFrom);
            RuleFor(_ => _.TierFrom).NotNull().NotEmpty().GreaterThan(0).LessThanOrEqualTo(7);
            RuleFor(_ =>_.TierTo).NotNull().NotEmpty().GreaterThanOrEqualTo(_ => _.TierFrom).LessThanOrEqualTo(7);
            RuleFor(_ => _.Sources).NotNull().NotEmpty();
            RuleFor(_ => _.CompanyId).NotNull().NotEmpty();
            RuleFor(_ => _.ResponsibleHrId).NotNull().NotEmpty();
        }
    }
}
