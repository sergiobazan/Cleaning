using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Shared;

public static class Ensure
{
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void MoneyAmountNotNegative(
        [NotNull] decimal value,
        [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value < 0)
        {
            throw new ArgumentException(paramName);
        }
    }
}