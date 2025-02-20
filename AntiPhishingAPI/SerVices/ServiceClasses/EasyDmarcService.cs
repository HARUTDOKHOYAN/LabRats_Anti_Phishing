using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using Newtonsoft.Json;
using System.Text;

namespace AntiPhishingAPI.SerVices.ServiceClasses
{
    public class EasyDmarcService : IEasyDmarc
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string _credentialURL = "https://api.easydmarc.com/v1/oauth/token";
        private const string _easyDmarcCheckUrl = "https://api.easydmarc.com/v1/phishing-url/check-text";

        public EasyDmarcService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private async Task<EasyDmarcCredentials> GetCredentialsForEasyDmarcRequest(string secret, string id, string grant)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _credentialURL);

            request.Headers.Add("accept", "application/json");

            var requestData = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("client_id", id),
            new KeyValuePair<string, string>("client_secret", secret),
            new KeyValuePair<string, string>("grant_type", grant)
        });

            request.Content = requestData;
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var easyDmarcCredentials = JsonConvert.DeserializeObject<EasyDmarcCredentials>(jsonString);

                return easyDmarcCredentials;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        public async Task<DbData> CheckLinkByEasyDmarcAsync(CheckingLink link)
        {
            DbData dbInstance = new DbData();
            try
            {
                EasyDmarcCredentials credentials = await GetCredentialsForEasyDmarcRequest(_configuration["EasyDmark:client_id"], _configuration["EasyDmark:client_secret"], _configuration["EasyDmark:grant_type"]);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _easyDmarcCheckUrl);

                request.Headers.Add("accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {credentials.AccessToken}");

                var requestData = new StringContent(link.Link, Encoding.UTF8, "application/json");

                request.Content = requestData;
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    EasyDmarcResponseDto linkData = JsonConvert.DeserializeObject<EasyDmarcResponseDto>(responseJson);
                    if (linkData.Result)
                    {
                        dbInstance.Dangerousity += 0.25;
                        dbInstance.EasyDmarcResponse = linkData;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {

            }
            return dbInstance;
        }
    }
}
