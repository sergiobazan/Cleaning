using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Customers;

namespace Application.Customers.GetCustomer;

public sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, GetCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<GetCustomerResponse>> Handle(
        GetCustomerQuery request, 
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer is null)
        {
            return Result.Failure<GetCustomerResponse>(CustomerErrors.NotFound(request.Id));
        }

        var response = new GetCustomerResponse(customer.Id, customer.Name.Value, customer.Email.Value, customer.Phone.Value);
        return Result.Success(response);
    }
}
