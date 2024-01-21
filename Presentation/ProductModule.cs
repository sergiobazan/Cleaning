using Application.Products.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace Presentation;

public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (
            CreateProductRequest request, 
            IValidator<CreateProductCommand> validator, 
            ISender sender) =>
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Amount,
                request.Currency);

            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await sender.Send(command);

            if (result.IsFailure) return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        });
    }
}
