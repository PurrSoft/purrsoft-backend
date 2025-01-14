namespace PurrSoft.Application.QueryOverviews;

public class RequestOverview
{
	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public string Description { get; set; }
	public string RequestType { get; set; }
	public string UserId { get; set; }
}
