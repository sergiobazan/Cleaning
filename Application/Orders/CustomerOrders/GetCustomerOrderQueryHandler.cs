using Application.Abstractions.Behavior.Messaging;
using Application.Abstractions.Data;
using Domain.Abstractions;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.CustomerOrders;

public sealed class GetCustomerOrderQueryHandler : IQueryHandler<GetCustomerOrderQuery, List<Order>>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerOrderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Order>>> Handle(
        GetCustomerOrderQuery request, 
        CancellationToken cancellationToken)
    {
        return await _context.Orders.Where(o => o.CustomerId == request.CustomerId).ToListAsync();
    }
}
