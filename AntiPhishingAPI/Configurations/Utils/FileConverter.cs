using Microsoft.Identity.Client;

namespace AntiPhishingAPI.Configurations.Utils
{
    public static class FileConverter
    {
        public static async Task<ISet<String>> FileToSetConverter(string filePath)
        {
            ISet<String> SetofBlackList = new HashSet<String>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line=await reader.ReadLineAsync())!=null)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        SetofBlackList.Add(line);
                    }
                }
            }
            return SetofBlackList;
        }
    }
}
