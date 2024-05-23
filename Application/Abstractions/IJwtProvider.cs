using Domain.Customers;

namespace Application.Abstractions;

public interface IJwtProvider
{
    string Generate(Customer customer);
}
