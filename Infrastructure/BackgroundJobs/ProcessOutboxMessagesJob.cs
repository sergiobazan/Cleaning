using Domain.Abstractions;
using Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Beging to process outbox messages");

        List<OutboxMessage> messages = await GetOutboxMessages();

        if (messages.Count == 0)
        {
            _logger.LogInformation("Completed proccesing outbox messages - no messages to process");
            return;
        }

        foreach (var outboxMessage in messages)
        {
            Exception? exception = null;
            try
            {
                var domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(outboxMessage.Content, SerializerSettings)!;

                await _publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception caughtException)
            {
                _logger.LogError(
                    caughtException,
                    "Exception while proccesing outbox message {MessageId}",
                    outboxMessage.Id);

                exception = caughtException;
                outboxMessage.Error = caughtException.Message;
            }

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;

        }

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }

    private async Task<List<OutboxMessage>> GetOutboxMessages()
    {
        return await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(20)
            .ToListAsync();
    }

}
