using Domain.Abstractions;

namespace Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound(Guid id) => new(
        "Customers.NotFound", $"The customer with the Id = '{id}' was not found");

    public static Error AlreadyTaken(string email) => new(
        "Customers.AlreadyTaken", $"The customer with the Email = '{email}' is already taken");
}
