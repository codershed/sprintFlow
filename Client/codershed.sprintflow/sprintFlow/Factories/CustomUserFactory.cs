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
        var claimsIdentity = (ClaimsIdentity)user.Identity;

        if (account != null && account.AdditionalProperties.TryGetValue("roles", out var roles))
        {
            if (roles is JsonElement element && element.ValueKind == JsonValueKind.Array)
            {
                // Unpack the JSON array into individual C# claims
                foreach (var item in element.EnumerateArray())
                {
                    claimsIdentity.AddClaim(new Claim(options.RoleClaim, item.GetString()));
                }
            }
        }
        return user;
    }
}