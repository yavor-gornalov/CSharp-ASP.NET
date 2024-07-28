namespace GameZone.Infrastructure.Validations;

public static class GlobalConstants
{
	// Game
	public const int GameTitleMinLength = 2;
	public const int GameTitleMaxLength = 30;
	public const int GameDescriptionMinLength = 10;
	public const int GameDescriptionMaxLength = 500;

	// Genre
	public const int GenreNameMinLength = 3;
	public const int GenreNameMaxLength = 25;

	// DateTime
	public const string DateTimeDefaultFormat = "yyyy-MM-dd";
	public const string DateTimeRegex = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";
}
