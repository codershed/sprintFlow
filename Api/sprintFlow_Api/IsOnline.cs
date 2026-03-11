using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace sprintFlow_Api;

public class IsOnline
{
    private readonly ILogger<IsOnline> _logger;

    public IsOnline(ILogger<IsOnline> logger)
    {
        _logger = logger;
    }

    [Function("IsOnline")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed the IsOnline request.");
        return new OkObjectResult("sprintFlow-Api is online!");
    }
}