using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using sprintFlow;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{

    var auth = options.ProviderOptions.Authentication;
    auth.Authority = "https://codershedext.ciamlogin.com/";
    auth.ClientId = "a07a2725-95d5-431c-bc4a-458710aab394";
    auth.ValidateAuthority = true;

    options.UserOptions.NameClaim = "name";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.ScopeClaim = "scp";

    // Request the API scope so MSAL returns an access token for your API
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://2b3be717-1fb7-46a0-b298-bc47bdc9ef56/api.access");

}).AddAccountClaimsPrincipalFactory<CustomUserFactory>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("CanManageProjects", policy =>
        policy.RequireRole("Application.Administrator", "Project.Manager"));
    options.AddPolicy("CanManageTasks", policy =>
        policy.RequireRole("Application.Administrator", "Project.Manager", "Task.Manager"));
});

builder.Services.AddHttpClient("api", client =>
    client.BaseAddress = new Uri("http://localhost:7088"))
    .
AddHttpMessageHandler(sp =>
{
    return sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { "http://localhost:7088" },
            scopes: new[] { "api://2b3be717-1fb7-46a0-b298-bc47bdc9ef56/api.access" });
});




await builder.Build().RunAsync();
