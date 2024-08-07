namespace HouseRentingSystem.Infrastructure.Common;

public static class ValidationConstants
{
	// Category
	public const int CategoryNameMaxLength = 50;

	// House
	public const int HouseTitleMinLength = 10;
	public const int HouseTitleMaxLength = 50;
	public const int HouseAddressMinLength = 30;
	public const int HouseAddressMaxLength = 150;
	public const int HouseDescriptionMinLength = 50;
	public const int HouseDescriptionMaxLength = 500;
	public const string HousePricePerMonthMinValue = "0.0";
	public const string HousePricePerMonthMaxValue = "2000.0";

	// Agent
	public const int AgentPhoneNumberMinLength = 7;
	public const int AgentPhoneNumberMaxLength = 15;
}
