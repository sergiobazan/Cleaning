using Application.Abstractions.Messaging;

namespace Application.Products.Create;

public sealed record CreateProductCommand(string Name, decimal Amount, string Currency) : ICommand<Guid>;

public sealed record CreateProductRequest(string Name, decimal Amount, string Currency);