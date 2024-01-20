using Application.Abstractions.Behavior.Messaging;

namespace Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid Id) : IQuery<GetCustomerResponse>;