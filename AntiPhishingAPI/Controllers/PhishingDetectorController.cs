using System.Net;
using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
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
        public PhishingDetectorController(ICheckStatus checkStatus , IPhishingChecker checker, IVirusTotalService virusTotalService)
        {
            _checkStatus = checkStatus;
            _blackListChecker = checker;
            _virusTotalService = virusTotalService;
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
    }
}
