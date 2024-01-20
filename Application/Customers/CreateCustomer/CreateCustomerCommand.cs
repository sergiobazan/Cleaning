using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;


namespace Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(CreateCustomerRequest customer) : ICommand<CustomerCreatedResponse>;