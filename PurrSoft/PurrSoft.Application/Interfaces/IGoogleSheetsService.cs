using PurrSoft.Application.Models;

namespace PurrSoft.Application.Interfaces;

public interface IGoogleSheetsService
{
    Task<List<GoogleFormsResponseDto>> GetGoogleFormsResponsesAsync();
}

