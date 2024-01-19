using Application.Abstractions;
using Domain.Customers;
using Domain.Customers.Events;
using MediatR;

namespace Application.Customers.CreateCustomer;

public sealed class CreateCustomerDomainEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerDomainEventHandler(IEmailService emailService, ICustomerRepository customerRepository)
    {
        _emailService = emailService;
        _customerRepository = customerRepository;
    }

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(notification.Id);

        if (customer is null)
        {
            return;
        }
        await _emailService.SendEmail(customer.Email.Value, "New Customer", "You're now a customer of Kings");
    }
}
