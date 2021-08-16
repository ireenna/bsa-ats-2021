using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Stages.Dtos;
using FluentValidation;

namespace Application.Stages.Commands
{
    public class CreateVacancyStageCommandValidator : AbstractValidator<CreateVacancyStageCommand>
    {
        public CreateVacancyStageCommandValidator()
        {
            RuleFor(x => x.StageCreate).NotNull().SetValidator(new StageCreateDtoValidator());
        }
    }
}
