using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class OrderRepository(
    ApplicationDbContext _context) : IOrderRepository
{
    public void Add(Order order)
    {
        _context.Add(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Order>().FindAsync(id);
    }

    public async Task<List<Order>> GetOrdersByCustomerId(Guid customerId)
    {
        return await _context.Set<Order>()
            .Where(order => order.CustomerId == customerId)
            .ToListAsync();
    }
}