using Application.Abstractions.Behavior.Messaging;
using Application.Abstractions.Data;
using Domain.Abstractions;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.GetCustomer;

public sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, GetCustomerResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(IApplicationDbContext context, ICustomerRepository customerRepository)
    {
        _context = context;
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

    //public async Task<Result<GetCustomerResponse>> Handle(
    //    GetCustomerQuery request, 
    //    CancellationToken cancellationToken)
    //{
    //    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

    //    if (customer is null)
    //    {
    //        return Result.Failure<GetCustomerResponse>(CustomerErrors.NotFound(request.Id));
    //    }

    //    var response = new GetCustomerResponse(customer.Id, customer.Name.Value, customer.Email.Value, customer.Phone.Value);
    //    return Result.Success(response);
    //}
}
