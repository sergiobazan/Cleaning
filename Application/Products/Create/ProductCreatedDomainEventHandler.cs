using Domain.Products.Events;
using MediatR;

namespace Application.Products.Create;

public sealed class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
{
    public Task Handle(
        ProductCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
