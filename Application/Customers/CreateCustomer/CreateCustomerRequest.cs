﻿namespace Application.Customers.CreateCustomer;

public sealed record CreateCustomerRequest(
    string Name,
    string Email,
    string Phone);