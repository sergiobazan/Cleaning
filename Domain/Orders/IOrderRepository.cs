﻿using Domain.Products;

namespace Domain.Orders;

public interface IOrderRepository
{
    void Add(Order order);

    Task<Order?> GetByIdAsync(Guid id);

    Task<List<Order>> GetOrdersByCustomerId(Guid customerId);
}
