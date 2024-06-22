namespace Domain.Customers;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Customer> Customers = new();
}
