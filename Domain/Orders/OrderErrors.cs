using Domain.Abstractions;

namespace Domain.Orders;

public static class OrderErrors
{
    public readonly static Error OrderAlreadyCompleted = new(
        "Order.Completed", "Can't cancel an order that is already completed");

    public readonly static Error OrderIsNotCreated = new(
        "Order.NotCreated", "Can't Confirm an order that has not been created");
}
