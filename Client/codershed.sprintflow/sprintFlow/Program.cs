using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using sprintFlow;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://codershedext.ciamlogin.com/dafd6a95-45f6-4606-8f3d-b27cd917c979/v2.0";
    options.ProviderOptions.ClientId = "a07a2725-95d5-431c-bc4a-458710aab394";

    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.PostLogoutRedirectUri = builder.HostEnvironment.BaseAddress + "authentication/logout-callback";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.NameClaim = "name";
    
    options.ProviderOptions.DefaultScopes.Add("offline_access");
}).AddAccountClaimsPrincipalFactory<CustomUserFactory>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("CanManageProjects", policy =>
        policy.RequireRole("Application.Administrator", "Project.Manager"));
    options.AddPolicy("CanManageTasks", policy =>
        policy.RequireRole("Application.Administrator", "Task.Manager"));
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
