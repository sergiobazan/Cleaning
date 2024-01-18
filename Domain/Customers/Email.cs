using Domain.Shared;

namespace Domain.Customers;

public sealed record Email
{
    public string Value { get; init; }

    private Email(string value) => Value = value;

    public static Email Create(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        return new Email(value);
    }
}
