using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AntiPhishingAPI.Data.DTO
{
    [Owned]
    public class EasyDmarcResponseDto
    {
        [JsonProperty("original_url")]
        public string OriginalUrl { get; set; }

        [JsonProperty("redirected_url")]
        public string RedirectedUrl { get; set; }

        [JsonProperty("phishing_probability")]
        public double PhishingProbability { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("uid")]
        public string PredictionUid { get; set; }
    }
}
