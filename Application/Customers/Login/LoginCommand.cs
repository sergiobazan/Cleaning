using Application.Abstractions.Behavior.Messaging;

namespace Application.Customers.Login;

public sealed record LoginCommand(string Email) : ICommand<string>;
