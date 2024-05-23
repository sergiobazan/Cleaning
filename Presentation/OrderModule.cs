using Application.Orders.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Application.Orders.CustomerOrders;

namespace Presentation;

public class OrderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/order", 
            async (CreateOrderRequest request, ISender sender) =>
            {
                var command = new CreateOrderCommand(request.CustomerId, request.ProductId, request.OrderDate);

                var result = await sender.Send(command);

                return Results.Ok(result.Value);
            });

        app.MapGet("/order/{customerId:guid}",
            async (Guid customerId, ISender sender) =>
            {
                var query = new GetCustomerOrderQuery(customerId);

                var result = await sender.Send(query);

                return Results.Ok(result.Value);
            });
    }
}
