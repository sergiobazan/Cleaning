using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentications;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}
