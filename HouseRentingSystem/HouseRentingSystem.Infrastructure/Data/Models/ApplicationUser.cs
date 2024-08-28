using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Common.ValidationConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [PersonalData]
    [MaxLength(UserFirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [PersonalData]
    [MaxLength(UserLastNameMaxLength)]
    public string LastName { get; set; } = null!;

    public Agent? Agent { get; set; }
}
