using FluentValidation;
using Order.Domain.Models;

namespace Order.Domain.Validations
{
    public class ClientValidation : AbstractValidator<ClientModel>
    {
        public ClientValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;


            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 30);
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty();
        }
    }
}
