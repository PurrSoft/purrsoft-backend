namespace PurrSoft.Application.Helpers;

public class GuidValidationHelper
{
	public static bool BeAValidGuid(string? guid) =>
		Guid.TryParse(guid, out _);
}
