﻿using Domain.Abstractions;

namespace Domain.Products.Events;

public sealed record ProductCreatedDomainEvent(Guid Id) : IDomainEvent;