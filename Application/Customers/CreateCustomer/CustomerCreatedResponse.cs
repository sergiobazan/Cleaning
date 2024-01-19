namespace Application.Customers.CreateCustomer;

public sealed record CustomerCreatedResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone);
