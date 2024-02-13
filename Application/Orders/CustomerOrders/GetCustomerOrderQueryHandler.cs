using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Orders;

namespace Application.Orders.CustomerOrders;

public sealed class GetCustomerOrderQueryHandler : IQueryHandler<GetCustomerOrderQuery, List<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetCustomerOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<List<Order>>> Handle(
        GetCustomerOrderQuery request, 
        CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrdersByCustomerId(request.CustomerId);
    }
}
