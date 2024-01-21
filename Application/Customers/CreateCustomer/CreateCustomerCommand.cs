using Application.Abstractions.Behavior.Messaging;


namespace Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(CreateCustomerRequest customer) : ICommand<CustomerCreatedResponse>;