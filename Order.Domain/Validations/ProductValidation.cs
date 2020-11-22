using FluentValidation;
using Order.Domain.Models;

namespace Order.Domain.Validations
{
    public class ProductValidation : AbstractValidator<ProductModel>
    {
        public ProductValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .Length(3, 30);

            RuleFor(x => x.SellValue)
                .NotEqual(0)
                .NotEmpty();
        }
    }
}
