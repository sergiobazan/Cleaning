using Domain.Customers;
using Domain.Customers.Events;
using FluentAssertions;

namespace Domain.UnitTest.Customers;

public class UserTest
{
    [Fact]
    public void Create_Should_ReturnUser_WhenParamatersAreValid()
    {
        // Arrange
        var name = new Name("Sergio Julio");
        var email = Email.Create("sergio@gmail.com");
        var phone = new Phone("940245769");

        // Act
        var customer = Customer.Create(name, email, phone);

        // Assert
        customer.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenParamatersAreValid()
    {
        // Arrange
        var name = new Name("Sergio Julio");
        var email = Email.Create("sergio@gmail.com");
        var phone = new Phone("940245769");

        // Act
        var customer = Customer.Create(name, email, phone);

        // Assert
        customer.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<CustomerCreatedDomainEvent>();
    }
}
