namespace SeminarHub.Data.Common;

public static class ValidationConstants
{
	// Seminar
	public const int SeminarTopicMinLength = 3;
	public const int SeminarTopicMaxLength = 100;
	public const int SeminarLecturerMinLength = 5;
	public const int SeminarLecturerMaxLength = 60;
	public const int SeminarDetailsMinLength = 10;
	public const int SeminarDetailsMaxLength = 500;
	public const int SeminarDurationLowLimit = 30;
	public const int SeminarDurationHighLimit = 180;

	// Category
	public const int CategoryNameMinLength = 3;
	public const int CategoryNameMaxLength = 50;

	// DateTime
	public const string DateTimeDefaultFormat = "dd/MM/yyyy HH:mm";
	public const string DateTimeFormatRegex = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4} ([01][0-9]|2[0-3]):([0-5][0-9])$";
}
