using FluentValidation;
using MS_Authentication.Domain.Entities;
using MS_Authentication.Domain.Interfaces;

namespace MS_Authentication.Application.Validation;

public class UserRoleValidation : AbstractValidator<UserRole>
{
    public UserRoleValidation(IUserRoleRepository userRoleRepository)
    {
        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(ValidationMessage.requiredField);

        RuleFor(r => r.RoleId)
            .NotEmpty()
            .WithMessage(ValidationMessage.requiredField);

        When(r => r.ValidationRegister, () =>
        {
            RuleFor(r => r.CreatedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);

            RuleFor(r => r.CreatedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);
        });

        When(r => !r.ValidationRegister, () =>
        {
            RuleFor(r => r.UserId)
                .MustAsync(userRoleRepository.UserExistAsync)
                .WithMessage(ValidationMessage.NotFound);

            RuleFor(r => r.RoleId)
                .MustAsync(userRoleRepository.UserExistAsync)
                .WithMessage(ValidationMessage.NotFound);

            RuleFor(r => r.ModifiedOn)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);
        });
    }
}
