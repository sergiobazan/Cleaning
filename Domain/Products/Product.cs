using Domain.Abstractions;
using Domain.Products.Events;

namespace Domain.Products;

public sealed class Product : Entity
{
    private Product(
        Guid id,
        Name name,
        Money price) : base(id)
    {
        Name = name;
        Price = price;
    }
    private Product() { }

    public Name? Name { get; private set; }
    public Money? Price { get; private set; }

    public static Result<Product> Create(Name name, Money price)
    {
        var product = new Product(Guid.NewGuid(), name, price);

        product.Raise(new ProductCreatedDomainEvent(product.Id));

        return product;
    }

}
