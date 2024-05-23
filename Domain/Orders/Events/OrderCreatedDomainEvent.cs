using Domain.Abstractions;

namespace Domain.Orders.Events;

public sealed record OrderCreatedDomainEvent(Guid Id) : IDomainEvent;