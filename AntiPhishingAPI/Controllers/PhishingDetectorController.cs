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
        public PhishingDetectorController(IPhishingChecker checker)
        {
           _blackListChecker = checker;
        }
        // GET: api/<PhishingDetectorController>
        [HttpGet]
        public async Task <CheckingLink> Get()
        {
                return null;
        }
    }
}
