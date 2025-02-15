using Newtonsoft.Json;

namespace AntiPhishingAPI.Data.DTO
{
    public class EasyDmarcCredentials
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
