namespace GameZone.Infrastructure.Validations;

public static class GlobalConstants
{
    // Game
    public const byte GameTitleMinLength = 2;
    public const byte GameTitleMaxLength = 30;
    public const byte GameDescriptionMinLength = 10;
    public const byte GameDescriptionMaxLength = 50;

    // Genre
    public const byte GenreNameMinLength = 3;
    public const byte GenreNameMaxLength = 25;

    // DateTime
    public const string DateTimeDefaultFormat = "yyyy-MM-dd";
}
