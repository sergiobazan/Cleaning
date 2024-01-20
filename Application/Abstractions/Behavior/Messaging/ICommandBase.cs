using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Behavior.Messaging;


public interface ICommand : IRequest, ICommandBase
{ }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase 
{ }

public interface ICommandBase
{
}
