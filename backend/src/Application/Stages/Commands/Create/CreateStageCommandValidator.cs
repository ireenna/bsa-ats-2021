using Application.Stages.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stages.Commands.Create
{
    public class CreateStageCommandValidator : AbstractValidator<CreateStageCommand>
    {
        public CreateStageCommandValidator()
        {
            RuleFor(x => x.StageCreate).NotNull().SetValidator(new StageCreateDtoValidator());
        }
    }
}
