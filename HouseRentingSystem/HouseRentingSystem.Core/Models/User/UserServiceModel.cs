namespace HouseRentingSystem.Core.Models.User;

public class UserServiceModel
{
    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool IsAgent { get; set; }
}
