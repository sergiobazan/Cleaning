using Domain.Orders;

namespace Infrastructure.Repositories;

internal class OrderRepository : Repository<Order> ,IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Order>().FindAsync(id);
    }
}