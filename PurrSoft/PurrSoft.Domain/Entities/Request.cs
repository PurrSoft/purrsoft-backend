using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Domain.Entities;

public class Request
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public RequestType RequestType { get; set; }
	public DateTime CreatedAt { get; set; }
	public string UserId { get; set; }
	public ApplicationUser User { get; set; }
	public List<string>? Images { get; set; }
}
