using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Customers;
using MediatR;

namespace Application.Customers.Update;

public sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<Guid>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.Id));
        }

        customer.Update(
            new Name(request.Customer.Name),
            Email.Create(request.Customer.Email),
            new Phone(request.Customer.Phone));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new CustomerUpdatedEvent(customer.Id), cancellationToken);

        return customer.Id;
    }
}
