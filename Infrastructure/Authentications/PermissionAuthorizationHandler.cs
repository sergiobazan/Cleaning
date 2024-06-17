using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Authentications;

public class PermissionAuthorizationHandler(IServiceScopeFactory serviceProvider) 
    : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAuthorizationRequirement requirement)
    {
        string? customerId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(customerId, out Guid parsedCustomerId))
        {
            return;
        }

        using IServiceScope scope = serviceProvider.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        HashSet<string> permissions = await permissionService.GetPermissionAsync(parsedCustomerId);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

    }
}
