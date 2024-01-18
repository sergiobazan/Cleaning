namespace Domain.Abstractions;

public abstract class Entity
{

    public Guid Id { get; init; }

    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id)
    {
        Id = id;
    }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
