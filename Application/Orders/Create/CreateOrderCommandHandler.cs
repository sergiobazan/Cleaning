using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Orders;

namespace Application.Orders.Create;

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(
            request.CustomerId,
            request.ProductId,
            request.OrderDate);

        if (order.IsFailure)
        {
            return Result.Failure<Guid>(order.Error);
        }

        _orderRepository.Add(order.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Value.Id;
    }
}
