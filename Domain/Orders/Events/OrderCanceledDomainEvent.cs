﻿using Domain.Abstractions;

namespace Domain.Orders.Events;

public sealed record OrderCanceledDomainEvent(Guid Id) : IDomainEvent;