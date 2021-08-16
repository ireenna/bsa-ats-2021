﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Enums;
using FluentValidation;

namespace Application.Stages.Dtos
{
    public class StageUpdateDto : Dto
    {
        public string Name { get; set; }
        public StageType Type { get; set; }
        public int Index { get; set; }
        public bool IsReviewable { get; set; }
        public string VacancyId { get; set; }
        public ICollection<ActionDto> Actions { get; set; }
    }
    public class StageUpdateDtoValidator : AbstractValidator<StageUpdateDto>
    {
        public StageUpdateDtoValidator()
        {
            RuleFor(_ => _.Name).NotNull().NotEmpty();
            RuleFor(_ => _.Type).NotNull().NotEmpty();
            RuleFor(_ => _.Index).NotNull().NotEmpty();
            RuleFor(_ => _.IsReviewable).NotNull().NotEmpty();
            RuleFor(_ => _.VacancyId).NotNull().NotEmpty();
            RuleFor(_ => _.Actions).NotNull().NotEmpty();
        }
    }
}
