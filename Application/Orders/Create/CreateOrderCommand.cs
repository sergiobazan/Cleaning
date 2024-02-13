using Application.Abstractions.Behavior.Messaging;

namespace Application.Orders.Create;

public sealed record CreateOrderCommand(Guid CustomerId, Guid ProductId, DateTime OrderDate) : ICommand<Guid>;

public sealed record CreateOrderRequest(Guid CustomerId, Guid ProductId, DateTime OrderDate);