using FluentValidation;

namespace Application.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(model => model.Currency)
            .NotEmpty()
            .Must((product, _) =>
        {
            return product.Currency.Length < 4;
        }).WithMessage("Currency must be less than 4 characters");

        RuleFor(model => model.Amount)
            .NotEmpty()
            .Must((product, _) =>
        {
            return product.Amount > 0;
        }).WithMessage("Amount must be a positive value");
    }
}
