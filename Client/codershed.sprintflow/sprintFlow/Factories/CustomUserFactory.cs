using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor) { }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
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

        //var claimsIdentity = (ClaimsIdentity)user.Identity;

        //if (account != null && account.AdditionalProperties.TryGetValue("roles", out var roles))
        //{
        //    if (roles is JsonElement element && element.ValueKind == JsonValueKind.Array)
        //    {
        //        Unpack the JSON array into individual C# claims
        //        foreach (var item in element.EnumerateArray())
        //        {
        //            claimsIdentity.AddClaim(new Claim(options.RoleClaim, item.GetString()));

        //        }
        //    }
        //}
        return user;
    }
}