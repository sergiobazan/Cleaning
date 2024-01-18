using Domain.Abstractions;
using Domain.Customers.Events;

namespace Domain.Customers;

public class Customer : Entity
{
    private Customer(
        Guid id,
        Name name,
        Email email,
        Phone phone) 
        : base(id) 
    { 
        Name = name;
        Email = email;
        Phone = phone;
    }

    public Name? Name { get; private set; }
    public Email? Email { get; private set; }
    public Phone? Phone { get; private set; }

    public static Customer Create(Name name, Email email, Phone phone)
    {
        var customer = new Customer(Guid.NewGuid(), name, email, phone);

        customer.Raise(new CustomerCreatedDomainEvent(customer.Id));

        return customer;
    }

}
