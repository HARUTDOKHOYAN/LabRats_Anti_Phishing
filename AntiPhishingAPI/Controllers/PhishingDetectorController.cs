using System.Net;
using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AntiPhishingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhishingDetectorController : ControllerBase
    {
        private readonly ICheckStatus _checkStatus;
        private readonly IPhishingChecker _blackListChecker;
        private readonly IVirusTotalService _virusTotalService;
        private readonly ILinksService _linksService;
        private readonly IMapper _mapper;
        public PhishingDetectorController(ICheckStatus checkStatus , IPhishingChecker checker, IVirusTotalService virusTotalService,ILinksService linksService)
        {
            _checkStatus = checkStatus;
            _blackListChecker = checker;
            _virusTotalService = virusTotalService;
            _linksService = linksService;
        }
        // GET: api/<PhishingDetectorController>
        [HttpGet]
        public async Task<CheckingLink> Get()
        {
            var link = new CheckingLink{Link = "https://www.Kokalo.su/"};
            var status = await _checkStatus.GetStatus(link.Link);
            if (status != HttpStatusCode.OK)
            {
                link.Dangerousity = -1;
                return link;
            }
            await _blackListChecker.CheckLinkPresenceInPhishingDbAsync(link.Link);
            link = await _virusTotalService.CheckLinkInVirusTotalAsync(link);
            return link;
        }
        [HttpPost]
        public async Task<int>CheckLink(string link)
        {
            if (String.IsNullOrEmpty(link))
            {
                //can return another thing
                throw new Exception("incorrect data");
            }
            CheckingLink CheckLink=_mapper.Map<CheckingLink>(link);
            var status = await _checkStatus.GetStatus(CheckLink.Link);
            if (status != HttpStatusCode.OK)
            {
                CheckLink.Dangerousity = -1;
                return 0;
            }
            CheckedLink dbInstance= new CheckedLink() { Url = link };
            C
        }
    }
}
