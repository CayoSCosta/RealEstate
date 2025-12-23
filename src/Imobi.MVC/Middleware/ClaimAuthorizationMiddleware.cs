namespace Imobi.Middleware;
public class ClaimAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public ClaimAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint != null)
        {
            var requiredClaim = endpoint.Metadata.GetMetadata<RequireClaimAttribute>();

            if (requiredClaim != null)
            {
                var user = context.User;

                if (!user.Identity!.IsAuthenticated ||
                    !user.HasClaim(requiredClaim.ClaimType, "true"))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Acesso negado");
                    return;
                }
            }
        }

        await _next(context);
    }
}

public class RequireClaimAttribute : Attribute
{
    public string ClaimType { get; }
    public RequireClaimAttribute(string claimType)
    {
        ClaimType = claimType;
    }
}

