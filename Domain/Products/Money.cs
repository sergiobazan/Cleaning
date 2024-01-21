using Domain.Shared;

namespace Domain.Products;

public sealed record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public Money(decimal amount, string currency)
    {
        Ensure.NotNullOrEmpty(currency);
        Ensure.MoneyAmountNotNegative(amount);

        Amount = amount;
        Currency = currency;
    }

}