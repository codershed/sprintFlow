using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using sprintFlow_Api;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
        builder.UseMiddleware<AuthenticationMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Add the Auth services
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                options.Authority = "https://dafd6a95-45f6-4606-8f3d-b27cd917c979.ciamlogin.com/dafd6a95-45f6-4606-8f3d-b27cd917c979/v2.0";
                options.TokenValidationParameters.ValidAudiences = new[] {
                    "2b3be717-1fb7-46a0-b298-bc47bdc9ef56",
                    "a07a2725-95d5-431c-bc4a-458710aab394"
                };
                options.TokenValidationParameters.ValidIssuer = "https://dafd6a95-45f6-4606-8f3d-b27cd917c979.ciamlogin.com/dafd6a95-45f6-4606-8f3d-b27cd917c979/v2.0";
            }, options => {
                options.Instance = "https://dafd6a95-45f6-4606-8f3d-b27cd917c979.ciamlogin.com/";
                options.TenantId = "dafd6a95-45f6-4606-8f3d-b27cd917c979";
                options.ClientId = "2b3be717-1fb7-46a0-b298-bc47bdc9ef56";
            });

        services.AddAuthorization();
    })
    .Build();

host.Run();
