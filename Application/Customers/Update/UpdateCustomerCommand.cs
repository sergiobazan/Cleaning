using Application.Abstractions.Behavior.Messaging;

namespace Application.Customers.Update;

public sealed record UpdateCustomerCommand(Guid Id, UpdateCustomerRequest Customer) : ICommand<Guid>;

public sealed record UpdateCustomerRequest(
    string Name,
    string Email,
    string Phone);