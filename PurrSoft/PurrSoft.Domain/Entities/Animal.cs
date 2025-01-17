using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Domain.Entities;

public class Animal
{

    public Guid Id { get; set; }
    public AnimalType AnimalType { get; set; }
    public string? Name { get; set; }
    public int YearOfBirth { get; set; }
    public string? Gender { get; set; }
    public bool Sterilized { get; set; }
    public IList<string> ImageUrls { get; set; }
    public string? Passport {  get; set; }
    public virtual AnimalProfile? AnimalProfile { get; set; }
    public virtual IList<AnimalFosterMap> FosteredBy { get; set; }
    public virtual IList<Treatment> Treatments { get; set; } // Navigation property for Treatments

    public Animal()
    {
        FosteredBy = new List<AnimalFosterMap>();
        Treatments = new List<Treatment>(); // Initialize Treatments list
    }
}

