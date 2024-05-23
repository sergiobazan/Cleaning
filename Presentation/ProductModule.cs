using Application.Products.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Presentation;

public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Amount,
                request.Currency);

            var result = await sender.Send(command);

            return Results.Ok(result.Value);
        });
    }
}
