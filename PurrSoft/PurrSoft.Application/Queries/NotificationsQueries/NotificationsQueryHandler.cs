using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.NotificationsQueries
{
    public class NotificationsQueryHandler(IRepository<Notifications> notificationsRepository) :
        IRequestHandler<GetNotificationsQuery, CollectionResponse<NotificationsDto>>,
        IRequestHandler<GetNotificationByIdQuery, CommandResponse<NotificationsDto>>,
        IRequestHandler<GetUnreadNotificationsQuery, CollectionResponse<NotificationsDto>>,
        IRequestHandler<SearchNotificationsQuery, CollectionResponse<NotificationsDto>>,
        IRequestHandler<GetNotificationsByDateRangeQuery, CollectionResponse<NotificationsDto>>
    {
        public async Task<CollectionResponse<NotificationsDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var query = notificationsRepository.Query();

            if (!string.IsNullOrEmpty(request.UserId)) // Adjusted for string UserId
                query = query.Where(n => n.UserId == request.UserId);
            if (!string.IsNullOrEmpty(request.Type))
                query = query.Where(n => n.Type == request.Type);
            if (request.IsRead.HasValue)
                query = query.Where(n => n.IsRead == request.IsRead.Value);

            var result = await query
                .Select(n => MapToDto(n))
                .ToListAsync(cancellationToken);

            return new CollectionResponse<NotificationsDto>(result, result.Count);
        }

        public async Task<CommandResponse<NotificationsDto>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await notificationsRepository
                .Query(n => n.Id == request.NotificationId)
                .Select(n => MapToDto(n))
                .FirstOrDefaultAsync(cancellationToken);

            return notification == null
                ? CommandResponse.Failed<NotificationsDto>(new[] { "Notification not found." })
                : CommandResponse.Ok(notification);
        }

        public async Task<CollectionResponse<NotificationsDto>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
        {
            var query = notificationsRepository.Query().Where(n => !n.IsRead);

            if (!string.IsNullOrEmpty(request.UserId)) // Adjusted for string UserId
                query = query.Where(n => n.UserId == request.UserId);

            var result = await query
                .Select(n => MapToDto(n))
                .ToListAsync(cancellationToken);

            return new CollectionResponse<NotificationsDto>(result, result.Count);
        }

        public async Task<CollectionResponse<NotificationsDto>> Handle(SearchNotificationsQuery request, CancellationToken cancellationToken)
        {
            var query = notificationsRepository.Query();

            if (!string.IsNullOrEmpty(request.Message))
                query = query.Where(n => EF.Functions.Like(n.Message, $"%{request.Message}%"));

            var result = await query
                .Select(n => MapToDto(n))
                .ToListAsync(cancellationToken);

            return new CollectionResponse<NotificationsDto>(result, result.Count);
        }

        public async Task<CollectionResponse<NotificationsDto>> Handle(GetNotificationsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var query = notificationsRepository.Query();

            if (request.StartDate.HasValue)
                query = query.Where(n => n.CreatedAt >= request.StartDate.Value);
            if (request.EndDate.HasValue)
                query = query.Where(n => n.CreatedAt <= request.EndDate.Value);

            var result = await query
                .Select(n => MapToDto(n))
                .ToListAsync(cancellationToken);

            return new CollectionResponse<NotificationsDto>(result, result.Count);
        }

        private static NotificationsDto MapToDto(Notifications n) => new()
        {
            Id = n.Id,
            
            Type = n.Type,
            Message = n.Message,
            IsRead = n.IsRead
            
        };
    }
}
