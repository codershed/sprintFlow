using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace sprintFlow;

public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomUserFactory(IAccessTokenProviderAccessor accessor) 
        : base(accessor)
    {
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
        RemoteUserAccount account,
        RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (user.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
        {
            // Handle roles claim - it may come as a JsonElement array
            var rolesClaim = identity.FindFirst("roles");
            if (rolesClaim is not null && rolesClaim.Value.StartsWith('['))
            {
                identity.RemoveClaim(rolesClaim);

                var roles = JsonSerializer.Deserialize<string[]>(rolesClaim.Value);
                if (roles is not null)
                {
                    foreach (var role in roles)
                    {
                        identity.AddClaim(new Claim(identity.RoleClaimType, role));
                    }
                }
            }
            else if (rolesClaim is not null)
            {
                // Single role as string - add it as role claim type
                identity.RemoveClaim(rolesClaim);
                identity.AddClaim(new Claim(identity.RoleClaimType, rolesClaim.Value));
            }
        }

        return user;
    }
}