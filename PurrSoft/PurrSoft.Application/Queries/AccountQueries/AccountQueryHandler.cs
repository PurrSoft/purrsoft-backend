using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.AccountQueries;

public class AccountQueryHandler(IRepository<ApplicationUser> userRepository) :
    IRequestHandler<GetLoggedInUserQuery, ApplicationUserDto>,
    IRequestHandler<GetUsersByRoleQuery, CollectionResponse<ApplicationUserDto>>
{
    public async Task<ApplicationUserDto> Handle(GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        return await userRepository
            .Query(u => u.Id == request.User.UserId)
            .ProjectToDto()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CollectionResponse<ApplicationUserDto>>
        Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        List<ApplicationUserDto> UserList = await userRepository
            .Query(u => u.UserRoles
                .Any(role => role.Role.NormalizedName == request.Role))
            .ProjectToDto().ToListAsync(cancellationToken);
        return new CollectionResponse<ApplicationUserDto>(UserList, UserList.Count);
    }
}