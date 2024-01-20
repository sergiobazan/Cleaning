using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Customer>().FindAsync(id);
    }

    public async Task<bool> IsEmailAlreadyTakenAsync(string email)
    {
        var customers = await _context.Set<Customer>().ToListAsync();
        return customers.Any(customer => customer.Email!.Value == email);
    }
}
