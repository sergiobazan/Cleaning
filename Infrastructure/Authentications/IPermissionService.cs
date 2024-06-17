namespace Infrastructure.Authentications;

internal interface IPermissionService
{
    public Task<HashSet<string>> GetPermissionAsync(Guid clientId);
}
