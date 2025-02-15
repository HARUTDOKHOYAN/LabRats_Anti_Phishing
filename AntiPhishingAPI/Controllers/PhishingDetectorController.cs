using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using LearningASPweb.Data;
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
        // POST api/<PhishingDetectorController>
        [HttpPost]
        public ActionResult Post([FromBody] TextModel value)
        {
            // Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:5234/api/PhishingDetector");
            // Response.Headers.Append("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            return Ok(new { message = "CORS enabled for this endpoint" });
        }
    }
}
