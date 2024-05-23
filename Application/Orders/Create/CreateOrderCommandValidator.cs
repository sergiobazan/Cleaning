using FluentValidation;

namespace Application.Orders.Create;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(model => model.CustomerId)
            .NotEmpty();

        RuleFor(model => model.ProductId)
            .NotEmpty();
    }
}
