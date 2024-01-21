using Domain.Abstractions;

namespace Domain.Products;

public static class ProductErrors
{
    public static Error PriceCurrencyError() => new(
        "Product.PriceCurrencyError", "The given currency is bad formatted");
}
