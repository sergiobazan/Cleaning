namespace Application.Customers.GetCustomer;

public sealed record GetCustomerResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone);