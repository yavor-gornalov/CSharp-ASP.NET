using HouseRentingSystem.Core.Contracts.House;
using System.ComponentModel;

namespace HouseRentingSystem.Core.Models.House;

public class HouseServiceModel : IHouseModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Address { get; set; } = null!;

    [DisplayName("Image URL")]
    public string ImageUrl { get; set; } = null!;

    [DisplayName("Price Per Month")]
    public decimal PricePerMonth { get; set; }

    [DisplayName("Is Rented")]
    public bool IsRented { get; set; }
}
