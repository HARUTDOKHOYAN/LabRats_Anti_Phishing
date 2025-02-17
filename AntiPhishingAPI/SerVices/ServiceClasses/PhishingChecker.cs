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
        public PhishingChecker(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<CheckingLink> CheckLinkPresenceInPhishingDbAsync(string phishingLink)
        {
            string blackListPath = _configuration["BlacklIstsFiles:PhisngLinksPath"];
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
