using Application.Abstractions;
using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Customers;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler 
    : ICommandHandler<CreateCustomerCommand, CustomerCreatedResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IJwtProvider jwtProvider)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _jwtProvider = jwtProvider;
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

        Role? role;

        role = await _roleRepository.FindByNameAsync("Customer");

        if (role is null)
        {
            role = await _roleRepository.CreateAsync("Customer");
        }

        customer.Value.AddRoles(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtProvider.Generate(customer.Value);

        var response = new CustomerCreatedResponse(customer.Value.Id, token);

        return response;
    }
}
