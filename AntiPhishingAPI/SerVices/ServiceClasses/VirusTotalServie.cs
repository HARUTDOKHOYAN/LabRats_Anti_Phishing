using AntiPhishingAPI.Configurations.Utils;
using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using System.Text.Json;

namespace AntiPhishingAPI.SerVices.ServiceClasses
{
    public class VirusTotalServie : IVirusTotalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _xapiKey;

        public VirusTotalServie(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _xapiKey = _configuration["VirusTotalSettings:ApiKey"];
        }
        private async Task<string> CheckLinkByIdFromVirusTotalAsync(string id)
        {
            var requestUrl = $"https://www.virustotal.com/api/v3/analyses/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Add("x-apikey", _xapiKey);
            request.Headers.Add("accept", "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        private async Task<string> SendLinkToVirusTotalToCheckAsync(string requestURL)
        {
            var virusTotalUrl = "https://www.virustotal.com/api/v3/urls";
            var request = new HttpRequestMessage(HttpMethod.Post, virusTotalUrl);

            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            request.Headers.Add("x-apikey", _xapiKey);

            var postData = new List<KeyValuePair<string, string>>
            {
            new KeyValuePair<string, string>("url", requestURL)
            };

            request.Content = new FormUrlEncodedContent(postData);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonSerializer.Deserialize<JsonElement>(json);
            var id = jsonObject.GetProperty("data").GetProperty("id").GetString();

            return id;
        }

        private  async Task<double> VirusTotalChecksResultAsync(VirusTotalResultDTO dto)
        {
            //I mean this logic
            return dto.Harmless-(dto.Suspicious+dto.Malicious+dto.Undetected);
        }

        public async Task<CheckingLink> CheckLinkInVirusTotalAsync(CheckingLink link)
        {
            string checkingLinkId = await SendLinkToVirusTotalToCheckAsync(link.Link);
            string virusTotalServiceResponse= await CheckLinkByIdFromVirusTotalAsync(checkingLinkId);
            VirusTotalResultDTO virusTotalResultDTO = await Converter.JsonToVirusTotalObjectConverter(virusTotalServiceResponse);
            //the logic is so simple can be improved further
            if (await VirusTotalChecksResultAsync(virusTotalResultDTO)<0)
            {
                link.Dangerousity += 0.25;
            }
            return link;
        }

    }
}
