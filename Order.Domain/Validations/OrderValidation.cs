using FluentValidation;
using Order.Domain.Models;

namespace Order.Domain.Validations
{
    public class OrderValidation : AbstractValidator<OrderModel>
    {
        public OrderValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Client)
                .NotNull();

            RuleFor(x => x.Items)
                .NotNull();

            RuleFor(x => x.Items.Count)
              .NotEqual(0);
        }
    }
}
