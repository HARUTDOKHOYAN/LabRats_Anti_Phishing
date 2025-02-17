using AntiPhishingAPI.Configurations.Utils;
using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using AutoMapper;

namespace AntiPhishingAPI.SerVices.ServiceClasses
{
    public class PhishingChecker : IPhishingChecker
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PhishingChecker(IMapper mapper, IConfiguration configuration , IWebHostEnvironment env)
        {
            
            _mapper = mapper;
            _configuration = configuration;
            _env = env;
        }

        public async Task<CheckingLink> CheckLinkPresenceInPhishingDbAsync(string phishingLink)
        {
            string blackListPath = Path.Combine(_env.ContentRootPath, "PhishingLinks/phishing-links-ACTIVE.txt");
            HashSet<string> blacklist = (HashSet<string>)await Converter.FileToSetConverter(blackListPath);
            CheckingLink link=_mapper.Map<CheckingLink>(phishingLink);
            if (blacklist.Contains(link.Link)) 
            {
                link.Dangerousity += 1;
            }
            return link;
        }
    }
}
