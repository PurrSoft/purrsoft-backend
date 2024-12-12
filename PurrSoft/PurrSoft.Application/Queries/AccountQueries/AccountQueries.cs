using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.AccountQueries;

public class GetLoggedInUserQuery : BaseRequest<ApplicationUserDto>
{
}

public class GetUsersByRoleQuery : BaseRequest<CollectionResponse<ApplicationUserDto>>
{
    public string Role { get; set; }
}

public class GetRolesAndStatusesByUserIdQuery : BaseRequest<CollectionResponse<UserRoleStatusDto>>
{
    public string Id { get; set; }
}

public class GetRolesAndDatesByUserIdQuery : BaseRequest<CollectionResponse<UserRoleDatesDto>>
{
	public string Id { get; set; }
}