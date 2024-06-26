﻿using Domain.Abstractions;
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
    public List<Role> Roles = new();

    public static Result<Customer> Create(Name name, Email email, Phone phone)
    {
        var customer = new Customer(Guid.NewGuid(), name, email, phone);

        customer.Raise(new CustomerCreatedDomainEvent(customer.Id));

        return customer;
    }

    public void Update(Name name, Email email, Phone phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public void AddRoles(Role role)
    {
        Roles.Add(role);
    }
}
