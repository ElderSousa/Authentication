using FluentValidation;
using MS_Authentication.Application.Requests;

namespace MS_Authentication.Application.Validation;

public class LoginValidation : AbstractValidator<AuthRequest>
{
    public LoginValidation()
    {
        RuleFor(l => l.Email)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField)
                .EmailAddress()
                .WithMessage(ValidationMessage.InvalidEmail);

        RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage(ValidationMessage.requiredField)
                .MinimumLength(8)
                .WithMessage("A senha deve conter no mínimo 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
                .Matches(@"[^\da-zA-Z]").WithMessage("A senha deve conter pelo menos um caractere especial.");
    }
}
