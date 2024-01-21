using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Behavior.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ 
}