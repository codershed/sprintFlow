using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace sprintFlow_Api;

public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        // Check if the function has [Authorize] attribute
        var targetMethod = context.GetTargetFunctionMethod();
        var authorizeAttribute = targetMethod?.GetCustomAttribute<AuthorizeAttribute>() 
            ?? targetMethod?.DeclaringType?.GetCustomAttribute<AuthorizeAttribute>();

        if (authorizeAttribute != null)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext != null)
            {
                var authService = httpContext.RequestServices.GetRequiredService<IAuthenticationService>();
                var result = await authService.AuthenticateAsync(httpContext, JwtBearerDefaults.AuthenticationScheme);

                if (!result.Succeeded)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await httpContext.Response.WriteAsync("Unauthorized");
                    return;
                }

                httpContext.User = result.Principal!;
            }
        }

        await next(context);
    }
}

public static class FunctionContextExtensions
{
    public static MethodInfo? GetTargetFunctionMethod(this FunctionContext context)
    {
        var entryPoint = context.FunctionDefinition.EntryPoint;
        var assemblyPath = context.FunctionDefinition.PathToAssembly;
        var assembly = Assembly.LoadFrom(assemblyPath);
        var typeName = entryPoint.Substring(0, entryPoint.LastIndexOf('.'));
        var methodName = entryPoint.Substring(entryPoint.LastIndexOf('.') + 1);
        var type = assembly.GetType(typeName);
        return type?.GetMethod(methodName);
    }
}