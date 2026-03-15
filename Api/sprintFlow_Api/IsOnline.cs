using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace sprintFlow_Api;

public class IsOnline
{
    private readonly ILogger<IsOnline> _logger;

    public IsOnline(ILogger<IsOnline> logger)
    {
        _logger = logger;
    }

    
    [Authorize] // Ensures the user is logged in
    [Function("IsOnline")]
    [RequiredScope("api.access")] // Matches the 'scp' claim in your token
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "isonline")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed the IsOnline request.");
        return new OkObjectResult(true);
    }
}