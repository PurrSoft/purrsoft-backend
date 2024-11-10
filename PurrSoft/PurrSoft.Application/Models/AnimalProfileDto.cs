namespace PurrSoft.Application.Models
{
    public class AnimalProfileDto
    {
        public Guid Id { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
        
        public Guid AnimalId { get; set; }
    }
}