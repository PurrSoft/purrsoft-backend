namespace PurrSoft.Application.Models;

public class AnimalDto
{
    public string? Id { get; set; }
    public string? AnimalType { get; set; }
    public string? Name { get; set; }
    public int YearOfBirth { get; set; }
    public string? Gender { get; set; }
    public Boolean Sterilized { get; set; }
    public string? ImageUrl { get; set; }
}