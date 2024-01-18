using System.Runtime.CompilerServices;

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

public static class Ensure
{
    public static void NotNullOrEmpty(
        string? value,
        [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }
}