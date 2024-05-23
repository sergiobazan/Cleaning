using Domain.Abstractions;
using Domain.Orders.Events;
using Domain.Products;

namespace Domain.Orders;

public sealed class Order : Entity
{
    private Order()
    {
    }

    private Order(
        Guid id,
        Guid customerId,
        Guid productId,
        DateTime orderDate,
        OrderStatus status) : base(id)
    {
        CustomerId = customerId;
        ProductId = productId;
        OrderDate = orderDate;
        Status = status;
    }

    public Guid CustomerId { get; private set; }
    public Guid ProductId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }

    public static Result<Order> Create(Guid customerId, Guid productId, DateTime orderDate)
    {
        var order = new Order(Guid.NewGuid(), customerId, productId, orderDate, OrderStatus.Created);

        order.Raise(new OrderCreatedDomainEvent(order.Id));

        return order;
    }

    public Result Complete()
    {
        if (Status != OrderStatus.Created)
        {
            return Result.Failure(OrderErrors.OrderIsNotCreated);
        }

        Status = OrderStatus.Completed;

        Raise(new OrderCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel()
    {
       if (Status != OrderStatus.Created)
       {
            return Result.Failure(OrderErrors.OrderAlreadyCompleted);
       }

        Status = OrderStatus.Canceled;

        Raise(new OrderCanceledDomainEvent(Id));

        return Result.Success();
    }
}
