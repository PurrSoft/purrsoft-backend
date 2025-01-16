namespace PurrSoft.Application.Models;

public class AnimalDto
{
    public string? Id { get; set; }
    public string? AnimalType { get; set; }
    public string? Name { get; set; }
    public int? YearOfBirth { get; set; }
    public string? Gender { get; set; }
    public bool? Sterilized { get; set; }
    public string? Passport { get; set; }
    public IList<string>? ImageUrls { get; set; }
}