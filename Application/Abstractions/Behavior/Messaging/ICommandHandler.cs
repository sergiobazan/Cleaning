using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Behavior.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand: ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand: ICommand<TResponse>
{ }