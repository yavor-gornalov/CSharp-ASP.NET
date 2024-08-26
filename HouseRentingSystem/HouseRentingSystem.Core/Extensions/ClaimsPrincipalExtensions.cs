using System.Security.Claims;
using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;

namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string Id(this ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static bool IsAdmin(this ClaimsPrincipal user)
        => user.IsInRole(AdminRoleName);
}
