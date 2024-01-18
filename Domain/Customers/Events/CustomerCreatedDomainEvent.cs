using Domain.Abstractions;

namespace Domain.Customers.Events;

public sealed record CustomerCreatedDomainEvent(Guid Id) : IDomainEvent;

