namespace System.Security.Claims;

public static class ClaimPricipalExtension
{
    public static string Id(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
