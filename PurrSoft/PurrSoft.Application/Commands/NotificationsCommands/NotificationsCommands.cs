using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Commands.NotificationCommands
{
    public class NotificationCommands
    {
        // Command to create a new Notification
        public class NotificationCreateCommand : BaseRequest<CommandResponse<Guid>>
        {
            public string UserId { get; set; } // ID of the user associated with the notification
            public string Type { get; set; } // Notification type (e.g., "Info", "Warning")
            public string Message { get; set; } // Notification message content
        }

        // Command to update an existing Notification
        public class NotificationUpdateCommand : BaseRequest<CommandResponse>
        {
            public Guid NotificationId { get; set; } // ID of the notification to update
            public string? Type { get; set; } // Updated notification type (optional)
            public string? Message { get; set; } // Updated notification message content (optional)
            public bool? IsRead { get; set; } // Mark the notification as read or unread (optional)
        }

        // Command to delete an existing Notification
        public class NotificationDeleteCommand : BaseRequest<CommandResponse>
        {
            public Guid NotificationId { get; set; } // ID of the notification to delete
        }
    }
}