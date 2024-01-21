using Domain.Customers;
using FluentValidation;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(c => c.customer.Email)
            .NotEmpty()
            .MustAsync(async (email, _) =>
        {
            return !await customerRepository.IsEmailAlreadyTakenAsync(email);
        }).WithMessage("The email must be unique");

        RuleFor(c => c.customer.Phone)
            .NotEmpty()
            .Length(9);
    }
}

