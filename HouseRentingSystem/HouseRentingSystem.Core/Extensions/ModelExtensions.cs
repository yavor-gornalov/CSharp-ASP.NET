using HouseRentingSystem.Core.Contracts.House;
using System.Net;
using System.Text.RegularExpressions;

namespace HouseRentingSystem.Core.Extensions;

public static class ModelExtensions
{
    public static string GetInformation(this IHouseModel house)
    {
        var info = house.Title.Replace(" ", "-") + "-" + GetAddress(house.Address);
        return Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

    }

    private static string GetAddress(string address)
       => string.Join("-", address.Split(' ').Take(3));
}
