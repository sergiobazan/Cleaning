using Domain.Customers;
using MediatR;

namespace Application.Customers.GetCustomer;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerResponse> Handle(
        GetCustomerQuery request, 
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer is null)
        {
            throw new Exception($"Customer with {request.Id} not found");
        }

        return new GetCustomerResponse(customer.Id, customer.Name.Value, customer.Email.Value, customer.Phone.Value);
    }
}
