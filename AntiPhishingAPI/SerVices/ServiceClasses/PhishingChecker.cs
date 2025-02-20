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
        private  HashSet<string> _blackList;
        public PhishingChecker(IConfiguration configuration , IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public async Task LoadPhishingLinks()
        {
            if(_blackList != null) return;
            string blackListPath = Path.Combine(_env.ContentRootPath, "PhishingLinks/phishing-links-ACTIVE.txt");
            _blackList =  (HashSet<string>)await Converter.FileToSetConverter(blackListPath);
        }
        public async Task<CheckingLink> CheckLinkPresenceInPhishingDbAsync(CheckingLink phishingLink,DbData dbInstance)
        {
            await LoadPhishingLinks();
            if (_blackList.Contains(phishingLink.Link)) 
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
