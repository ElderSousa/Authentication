using FluentValidation;
using MS_Authentication.Domain.Entities;
using MS_Authentication.Domain.Interfaces;

namespace MS_Authentication.Application.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation(IUserRepository userRepository)
        {
            RuleFor(u => u.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField);

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField)
                .EmailAddress()
                .WithMessage(ValidationMessage.InvalidEmail);

            RuleFor(u => u.PasswordHash)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField)
                .MinimumLength(8)
                .WithMessage("A senha deve conter no mínimo 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
                .Matches(@"[^\da-zA-Z]").WithMessage("A senha deve conter pelo menos um caractere especial.");

            RuleFor(u => u.typeUserRole)
                .IsInEnum()
                .WithMessage(ValidationMessage.requiredField);

            When(u => u.ValidationRegister, () =>
            {
                RuleFor(u => u.CreatedOn)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);
                
                RuleFor(u => u.CreatedOn)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);
            });

            When(u => !u.ValidationRegister, () =>
            {
                RuleFor(u => u.Id)
                    .MustAsync(userRepository.UserExistAsync)
                    .WithMessage(ValidationMessage.NotFound);

                RuleFor(u => u.ModifiedOn)
                    .NotEmpty()
                    .WithMessage(ValidationMessage.requiredField);
            });
        }
    }
}
