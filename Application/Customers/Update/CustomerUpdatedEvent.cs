using MediatR;

namespace Application.Customers.Update;

public sealed record CustomerUpdatedEvent(Guid CustomerId) : INotification;