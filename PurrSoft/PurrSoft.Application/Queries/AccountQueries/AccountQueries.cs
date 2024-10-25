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