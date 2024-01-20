using Domain.Abstractions;
using MediatR;

namespace Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(CreateCustomerRequest customer) : IRequest<Result<CustomerCreatedResponse>>;