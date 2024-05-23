using Domain.Abstractions;
using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Infrastructure.Interceptor;

public sealed class OutboxMessagesInterceptor : SaveChangesInterceptor
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext is not null)
        {
            InsertOutboxMessages(dbContext);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext dbContext)
    {
        DateTime utcNow = DateTime.UtcNow;
        List<OutboxMessage> outboxMessages = dbContext.ChangeTracker
           .Entries<Entity>()
           .Select(e => e.Entity)
           .SelectMany(e =>
           {
               var domainEvents = e.DomainEvents;

               e.ClearDomainEvents();

               return domainEvents;
           })
           .Select(domainEvent => new OutboxMessage
           { 
               Id = Guid.NewGuid(),
               Type = domainEvent.GetType().Name,
               Content =JsonConvert.SerializeObject(domainEvent, SerializerSettings),
               OccurredOnUtc = utcNow
            })
           .ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}
