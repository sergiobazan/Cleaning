using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Products;

namespace Application.Products.Create;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var result = Product.Create(new Name(request.Name), new Money(request.Amount, request.Currency));

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        _productRepository.Add(result.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}