using Domain.Customers;
using Infrastructure.Cache;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly ICacheService _cacheService;
    public CustomerRepository(ApplicationDbContext context, ICacheService cacheService)
        : base(context)
    {
        _cacheService = cacheService;
    }

    public async Task<Customer?> GetByEmailAsync(Email email)
    {
       return await _context.Set<Customer>().FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _cacheService.GetAsync(
            $"customer-{id}",
            async () =>
            {
                return await _context
                    .Set<Customer>()
                    .FirstOrDefaultAsync(c => c.Id == id);
            });
    }

    public async Task<bool> IsEmailAlreadyTakenAsync(string email)
    {
        return await _context.Set<Customer>().AnyAsync(customer => customer.Email == Email.Create(email));
    }
}
