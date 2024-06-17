namespace Domain.Customers;

public interface IRoleRepository
{
    Task<Role?> FindByNameAsync(string name);
    Task<Role> CreateAsync(string name);
}
