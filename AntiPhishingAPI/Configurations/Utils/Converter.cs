using AntiPhishingAPI.Data.DTO;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace AntiPhishingAPI.Configurations.Utils
{
    public static class Converter
    {
        public static async Task<ISet<String>> FileToSetConverter(string filePath)
        {
            ISet<String> SetofBlackList = new HashSet<String>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        SetofBlackList.Add(line);
                    }
                }
            }
            return SetofBlackList;
        }
        public static async Task<VirusTotalResultDTO> JsonToVirusTotalObjectConverter(string json)
        {
            VirusTotalResultDTO result= new VirusTotalResultDTO();
            try
            {
                var jsonObject = JObject.Parse(json);

                var stats = jsonObject.SelectToken("data.attributes.stats").ToString();

                result = JsonSerializer.Deserialize<VirusTotalResultDTO>(stats);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine($"Argument Error: {ex.Message}");
            }
            return result;
        }
    }
}
