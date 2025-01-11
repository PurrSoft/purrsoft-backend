using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurrSoft.Api.Controllers.Base;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.Queries.GoogleFormsResponsesQueries;
using System.Net;

namespace PurrSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoogleFormsController : BaseController
{
    public GoogleFormsController() { }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
    [ProducesResponseType(typeof(CollectionResponse<GoogleFormsResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<CollectionResponse<GoogleFormsResponseDto>> GetGoogleFormsResponses([FromQuery] GoogleFormsResponsesQueries query)
    {
        return await Mediator.Send(query, new CancellationToken());
    }
}
