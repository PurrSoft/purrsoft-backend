using MediatR;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.GoogleFormsResponsesQueries;

public class GoogleFormsResponsesQueryHandler(IGoogleSheetsService googleSheetsService,
    ILogRepository<GoogleFormsResponsesQueryHandler> _logRepository)
    : IRequestHandler<GoogleFormsResponsesQueries, CollectionResponse<GoogleFormsResponseDto>>
{
    public async Task<CollectionResponse<GoogleFormsResponseDto>> Handle(GoogleFormsResponsesQueries request, CancellationToken cancellationToken)
    {
        try
        {
            List<GoogleFormsResponseDto> formResponses = await googleSheetsService.GetGoogleFormsResponsesAsync();
            return new CollectionResponse<GoogleFormsResponseDto>(formResponses, formResponses.Count);
        } 
        catch (Exception e)
        {
            _logRepository.LogException(LogLevel.Error, e);
            throw;
        }
    }
}

