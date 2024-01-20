using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Behavior.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
