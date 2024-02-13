using Domain.Abstractions;

namespace Domain.Orders.Events;

public sealed record OrderCompletedDomainEvent(Guid Id) : IDomainEvent;