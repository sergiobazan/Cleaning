
using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentications;

internal class PermissionService(ApplicationDbContext context) : IPermissionService
{
    public async Task<HashSet<string>> GetPermissionAsync(Guid clientId)
    {

        ICollection<Role>[] roles = await context
            .Customers
            .Include(c => c.Roles)
            .Where(c => c.Id == clientId)
            .Select(c => c.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
