using System.Net;
using System.Text.RegularExpressions;
using AntiPhishingAPI.SerVices.ServiceInterfaces;

namespace AntiPhishingAPI.SerVices.ServiceClasses;

public class CheckStatusService : ICheckStatus
{
    private readonly HttpClient _httpClient;

    public CheckStatusService( HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpStatusCode> GetStatus(string link)
    {
        if (!IsValidHttpUrl(link)) return HttpStatusCode.BadRequest;
        var request = new HttpRequestMessage(HttpMethod.Get, link);
        request.Headers.Add("accept", "application/json");
        try
        {
            var response = await _httpClient.SendAsync(request);
            return response.StatusCode;
        }
        catch (Exception)
        {
            return  HttpStatusCode.BadRequest;
        }
    }
    public static bool IsValidHttpUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        string pattern = @"^(https?:\/\/)[^\s/$.?#].[^\s]*$";
        return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
    }
}