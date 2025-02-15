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

        public async Task<CheckingLink> CheckLinkPresenceInPhishingDBAsync(string phishingLink)
        {
            string BlackListPath = _configuration["BlacklIstsFiles:PhisngLinksPath"];
            HashSet<string> Blacklist = (HashSet<string>)await Converter.FileToSetConverter(BlackListPath);
            CheckingLink link=_mapper.Map<CheckingLink>(phishingLink);
            if (Blacklist.Contains(link.Link)) 
            {
                link.Dangerousity += 1;
            }
            return link;
        }
    }
}
