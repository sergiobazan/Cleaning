using Application.Customers.CreateCustomer;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Application.Customers.GetCustomer;
using Application.Customers.Login;
using Microsoft.AspNetCore.Authorization;

namespace Presentation;

public class CustomersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/customers", async (CreateCustomerRequest request, ISender sender) =>
        {
            var command = new CreateCustomerCommand(request);

            var result = await sender.Send(command);

            return Results.Ok(result.Value);
        });

        
        app.MapGet("/customers/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetCustomerQuery(id);
            var result = await sender.Send(query);

            if (result.IsFailure) return Results.NotFound(result.Error);

            return Results.Ok(result.Value);
        }).RequireAuthorization();

        app.MapPost("/customers/login", async (LoginRequest request, ISender sender) =>
        {
            var command = new LoginCommand(request.Email);

            var tokenResult = await sender.Send(command);

            if (tokenResult.IsFailure)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(tokenResult.Value);
        });
    }

}
