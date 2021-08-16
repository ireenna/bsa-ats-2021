using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Stages.Dtos;
using FluentValidation;

namespace Application.Stages.Commands
{
    public class EditVacancyStageCommandValidator : AbstractValidator<EditVacancyStageCommand>
    {
        public EditVacancyStageCommandValidator()
        {
            RuleFor(x => x.StageUpdate).NotNull().SetValidator(new StageUpdateDtoValidator());
        }
    }
}
