using Application.Abstractions;
using MediatR;

namespace Application.Customers.Update;

internal class CustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
{
    private readonly ICacheService _cacheService;

    public CustomerUpdatedEventHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync($"customer-{notification.CustomerId}");
    }
}
