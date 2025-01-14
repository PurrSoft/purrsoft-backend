using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.NotificationsQueries
{
    // Query to retrieve a collection of notifications
    public class GetNotificationsQuery : BaseRequest<CollectionResponse<NotificationsDto>>
    {
        // Optional filters based on relevant fields
        public string? UserId { get; set; } // Filter by user ID
        public string? Type { get; set; } // Filter by notification type
        public bool? IsRead { get; set; } // Filter by read/unread status
    }

    // Query to retrieve a single notification by its ID
    public class GetNotificationByIdQuery : BaseRequest<CommandResponse<NotificationsDto>>
    {
        public required Guid NotificationId { get; set; } // The ID of the notification to retrieve
    }

    // Query to search for notifications by message content
    public class SearchNotificationsQuery : BaseRequest<CollectionResponse<NotificationsDto>>
    {
        public string? Message { get; set; } // Search by message content
    }

    // Query to retrieve unread notifications
    public class GetUnreadNotificationsQuery : BaseRequest<CollectionResponse<NotificationsDto>>
    {
        public string? UserId { get; set; } // Filter by user ID
    }

    // Query to retrieve notifications within a specific date range
    public class GetNotificationsByDateRangeQuery : BaseRequest<CollectionResponse<NotificationsDto>>
    {
        public DateTime? StartDate { get; set; } // Start of the date range
        public DateTime? EndDate { get; set; } // End of the date range
    }
}