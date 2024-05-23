using Application.Abstractions.Behavior.Messaging;
using Domain.Orders;

namespace Application.Orders.CustomerOrders;

public sealed record GetCustomerOrderQuery(Guid CustomerId): IQuery<List<Order>>;

public sealed record GetCustomerOrderRequest(Guid CustomerId);