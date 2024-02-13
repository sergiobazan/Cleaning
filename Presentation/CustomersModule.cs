using Application.Customers.CreateCustomer;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Application.Customers.GetCustomer;

namespace Presentation;

public class CustomersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/customers", 
            async (CreateCustomerRequest request, ISender sender) =>
            {
                var command = new CreateCustomerCommand(request);

                var result = await sender.Send(command);

                return Results.Ok(result.Value);
            });

        app.MapGet("/customers/{id:guid}", 
            async (Guid id, ISender sender) =>
        {
            var query = new GetCustomerQuery(id);
            var result = await sender.Send(query);

            if (result.IsFailure) return Results.NotFound(result.Error);

            return Results.Ok(result.Value);
        });
    }

}
