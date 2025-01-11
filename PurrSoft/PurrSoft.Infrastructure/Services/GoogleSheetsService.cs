using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Common.Config;

namespace PurrSoft.Infrastructure.Services;

public class GoogleSheetsService(GoogleCredentialsConfig googleApiConfig, GoogleSheetsApiConfig googleSheetsApiConfig) : IGoogleSheetsService
{
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

    public async Task<List<GoogleFormsResponseDto>> GetGoogleFormsResponsesAsync()
    {
        SheetsService service = GetSheetsService();

        var range = $"{googleSheetsApiConfig.SheetName}!A2:Z";
        var request = service.Spreadsheets.Values.Get(googleSheetsApiConfig.SpreadsheetId, range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
        List<GoogleFormsResponseDto> formsResponses = new List<GoogleFormsResponseDto>();
        if (values != null && values.Count > 0)
        {
            foreach (var row in values)
            {
                GoogleFormsResponseDto googleFormsResponseDto = new GoogleFormsResponseDto
                {
                    Name = row[2].ToString() ?? "",
                    Email = row[1].ToString() ?? "",
                    Description = row[3].ToString() ?? "",
                };
                formsResponses.Add(googleFormsResponseDto);
            }
        } 
        else
        {
            throw new InvalidOperationException("No data found.");
        }
        return formsResponses;
    }

    private SheetsService GetSheetsService()
    {
        string json = JsonConvert.SerializeObject(googleApiConfig);
        Console.WriteLine(json);
        GoogleCredential credential = GoogleCredential
            .FromJson(json)
            .CreateScoped(Scopes);

        return new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = googleSheetsApiConfig.ApplicationName,
        });
    }
}

