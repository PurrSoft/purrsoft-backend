namespace PurrSoft.Application.DTOs
{
    public class AnimalProfileDto
    {
        public Guid Id { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
    }
}