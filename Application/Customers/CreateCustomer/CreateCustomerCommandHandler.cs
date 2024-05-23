using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Customers;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CustomerCreatedResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CustomerCreatedResponse>> Handle(
        CreateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            new Name(request.customer.Name),
            Email.Create(request.customer.Email),
            new Phone(request.customer.Phone));

        _customerRepository.Add(customer.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CustomerCreatedResponse(
            customer.Value.Id,
            customer.Value.Name!.Value,
            customer.Value.Email!.Value,
            customer.Value.Phone!.Value);

        return Result.Success(response);
    }
}
