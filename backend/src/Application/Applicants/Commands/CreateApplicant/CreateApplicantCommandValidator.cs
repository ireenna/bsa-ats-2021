using Application.Common.Files;
using Application.Common.Validators;
using FluentValidation;

namespace Application.Applicants.Commands.CreateApplicant
{
    public class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
    {
        public CreateApplicantCommandValidator()
        {
            RuleForEach(_ => _.CvFileDtos)
                .ExtensionMustBeInList(new FileExtension[] { FileExtension.Pdf })
                .When(_ => _.CvFileDtos != null);

            RuleFor(_ => _.PhotoFileDto)
                .ExtensionMustBeInList(new FileExtension[] {
                    FileExtension.Png,
                    FileExtension.Jpg,
                    FileExtension.Jpeg
                })
                .When(_ => _.PhotoFileDto != null);
        }
    }
}
