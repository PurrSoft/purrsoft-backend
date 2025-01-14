namespace PurrSoft.Application.QueryOverviews;

public class AnimalOverview
{
    public string? Id { get; set; }
    public string? AnimalType { get; set; }
    public string? Name { get; set; }
    public int YearOfBirth { get; set; }
    public string? Gender { get; set; }
    public Boolean Sterilized { get; set; }
    public ICollection<string> ImageUrls { get; set; }
}

