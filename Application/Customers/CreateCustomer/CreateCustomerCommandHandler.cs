using Domain.Abstractions;
using Domain.Customers;
using MediatR;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerCreatedResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerCreatedResponse> Handle(
        CreateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            new Name(request.customer.Name),
            Email.Create(request.customer.Email),
            new Phone(request.customer.Phone));

        if (customer is null)
        {
            return null;
        }

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync();

        return new CustomerCreatedResponse(customer.Id, customer.Name.Value, customer.Email.Value, customer.Phone.Value);
    }
}
