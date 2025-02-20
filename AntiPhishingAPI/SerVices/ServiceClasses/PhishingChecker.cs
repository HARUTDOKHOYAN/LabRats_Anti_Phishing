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
        private HashSet<string> _blackList;
        public PhishingChecker(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public async Task LoadPhishingLinks()
        {
            if (_blackList != null) return;
            string blackListPath = Path.Combine(_env.ContentRootPath, "PhishingLinks/phishing-links-ACTIVE.txt");
            _blackList = (HashSet<string>)await Converter.FileToSetConverter(blackListPath);
        }
        public async Task<DbData> CheckLinkPresenceInPhishingDbAsync(CheckingLink phishingLink)
        {
            await LoadPhishingLinks();
            DbData dbInstance = new DbData();
            if (_blackList.Contains(phishingLink.Link))
            {
                dbInstance.Dangerousity += 1;
                dbInstance.IsLinkInPhishingBlackList = true;
            }
            return dbInstance;
        }
    }
}
