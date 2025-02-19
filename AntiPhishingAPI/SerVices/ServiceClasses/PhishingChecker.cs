using AntiPhishingAPI.Configurations.Utils;
using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using AutoMapper;

namespace AntiPhishingAPI.SerVices.ServiceClasses
{
    public class PhishingChecker : IPhishingChecker
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PhishingChecker(IConfiguration configuration , IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public async Task<CheckingLink> CheckLinkPresenceInPhishingDbAsync(CheckingLink phishingLink,DbData dbInstance)
        {
            string blackListPath = Path.Combine(_env.ContentRootPath, "PhishingLinks/phishing-links-ACTIVE.txt");
            HashSet<string> blacklist = (HashSet<string>)await Converter.FileToSetConverter(blackListPath);
            if (blacklist.Contains(phishingLink.Link)) 
            {
                phishingLink.Dangerousity += 1;
                if (dbInstance != null)
                {
                    dbInstance.IsLinkInPhishingBlackList = true;
                }
            }
             return phishingLink;
        }
    }
}
