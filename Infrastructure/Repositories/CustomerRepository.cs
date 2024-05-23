using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CustomerRepository : Repository<Customer>, ICustomerRepository
{
   
    public CustomerRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<Customer?> GetByEmailAsync(Email email)
    {
       return await _context.Set<Customer>().FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Customer>().FindAsync(id);
    }

    public async Task<bool> IsEmailAlreadyTakenAsync(string email)
    {
        return await _context.Set<Customer>().AnyAsync(customer => customer.Email == Email.Create(email));
    }
}
