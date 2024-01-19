using MediatR;

namespace Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid Id) : IRequest<GetCustomerResponse>;