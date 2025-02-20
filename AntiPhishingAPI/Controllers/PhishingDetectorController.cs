using System.Net;
using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.SerVices.ServiceClasses;
using AntiPhishingAPI.SerVices.ServiceInterfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEasyDmarc _dmarc;
        public PhishingDetectorController(ICheckStatus checkStatus , IMapper mapper , IPhishingChecker checker, IVirusTotalService virusTotalService,ILinksService linksService, IEasyDmarc dmarc)
        {
            _mapper = mapper;
            _checkStatus = checkStatus;
            _blackListChecker = checker;
            _virusTotalService = virusTotalService;
            _linksService = linksService;
            _dmarc = dmarc;
        }
        //[Authorize]
        [HttpGet("Get/{id}")]
        public async Task<DbData> Get(int id)
        {
            DbData dbData=await _linksService.GetCheckingLinkById(id);
            return dbData;
        }
        //[Authorize]
        [HttpPost]
        public async Task<int>CheckLink([FromBody]LinkDto link)
        {
            if (String.IsNullOrEmpty(link.Url))
            { 
                //can return another thing
                throw new Exception("incorrect data");
            }
            var checkLink=_mapper.Map<CheckingLink>(link);
            DbData dbInstance = new DbData() { Url = link.Url };
            var status = await _checkStatus.GetStatus(checkLink.Link);
            //what if the 300 redirection comes??????
            if (status != HttpStatusCode.OK)
            {
                dbInstance.IsLinkActive = false;
                dbInstance.Dangerousity = 0;
                return await _linksService.AddLinkDataInDb(dbInstance);
            }
            checkLink=await _blackListChecker.CheckLinkPresenceInPhishingDbAsync(checkLink,dbInstance);
            checkLink = await _virusTotalService.CheckLinkInVirusTotalAsync(checkLink, dbInstance);
            await _dmarc.CheckLinkByEasyDmarcAsync(checkLink,dbInstance);
            return await _linksService.AddLinkDataInDb(dbInstance);
        }
    }
}
