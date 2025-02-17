using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AntiPhishingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhishingDetectorController : ControllerBase
    {
        private readonly IPhishingChecker _blackListChecker;
        private readonly IVirusTotalService _virusTotalService;
        public PhishingDetectorController(IPhishingChecker checker, IVirusTotalService virusTotalService)
        {
            _blackListChecker = checker;
            _virusTotalService = virusTotalService;
        }
        // GET: api/<PhishingDetectorController>
        [HttpGet]
        public async Task<CheckingLink> Get()
        {
            CheckingLink link = new CheckingLink() { Link = "https://www.google.com/" };
            await _blackListChecker.CheckLinkPresenceInPhishingDbAsync("https://www.google.com/");
            link = await _virusTotalService.CheckLinkInVirusTotalAsync(link);
            return link;
        }
    }
}
