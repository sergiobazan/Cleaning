using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    public async Task<Role> CreateAsync(string name)
    {
        var role = new Role() { Id = Guid.NewGuid(), Name = name };
        context.Set<Role>().Add(role);

        await context.SaveChangesAsync();

        return role;
    }

    public async Task<Role?> FindByNameAsync(string name)
    {
        return await context
            .Set<Role>()
            .FirstOrDefaultAsync(role => role.Name == name);
    }
}
