namespace Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);

    Task<bool> IsEmailAlreadyTakenAsync(string email);
    void Add(Customer customer);
}
