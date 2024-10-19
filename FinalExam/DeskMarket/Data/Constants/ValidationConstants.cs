namespace DeskMarket.Data.Constants;

public class ValidationConstants
{
    // Product Validations
    public const int ProductNameMinLength = 2;
    public const int ProductNameMaxLength = 60;

    public const int ProductDescriptionMinLength = 10;
    public const int ProductDescriptionMaxLength = 250;

    public const string ProductPriceMin = "1";
    public const string ProductPriceMax = "3000";

    public const int ProductImageUrlMaxLength = 2048;

    // Category Validations
    public const int CategoryNameMinLength = 3;
    public const int CategoryNameMaxLength = 20;

    // DateTime Format
    public const string DateTimeDefaultFormat = "dd-MM-yyyy";
}
