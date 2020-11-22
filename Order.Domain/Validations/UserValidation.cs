using FluentValidation;
using Order.Domain.Models;

namespace Order.Domain.Validations
{
    public class UserValidation : AbstractValidator<UserModel>
    {
        public UserValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 30);

            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PasswordHash)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
