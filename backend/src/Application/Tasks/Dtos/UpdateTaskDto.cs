using System;
using System.Collections.Generic;
using Application.Common.Models;
using FluentValidation;

namespace Application.Tasks.Dtos
{
    public class UpdateTaskDto : Dto
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime DueDate { get; set; }
        public string ApplicantId { get; set; }
        public string TeamMembersIds { get; set; }
    }

    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.DueDate).NotNull().NotEmpty();
            RuleFor(a => a.ApplicantId).NotNull().NotEmpty();            

        }
    }
}