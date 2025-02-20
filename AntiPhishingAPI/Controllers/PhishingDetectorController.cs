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
        public PhishingDetectorController(ICheckStatus checkStatus , IPhishingChecker checker, IVirusTotalService virusTotalService,ILinksService linksService, IEasyDmarc dmarc)
        {
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
        public async Task<int>CheckLink([FromBody]string link)
        {
            if (String.IsNullOrEmpty(link))
            {
                //can return another thing
                throw new Exception("incorrect data");
            }
            CheckingLink CheckLink=_mapper.Map<CheckingLink>(link);
            DbData dbInstance = await _linksService.GetLinkDataByURLAsync(CheckLink.Link);
            var status = await _checkStatus.GetStatus(CheckLink.Link);
            //what if the 300 redirection comes??????
            if (status != HttpStatusCode.OK)
            {
                dbInstance.IsLinkActive=false;
                dbInstance.Dangerousity = 1;
                await _linksService.AddLinkDataInDb(dbInstance);
                //the Front side prompts that the link does not exist in case of negative value
                return int.MinValue;
            }
            CheckLink=await _blackListChecker.CheckLinkPresenceInPhishingDbAsync(CheckLink,dbInstance);
            CheckLink = await _virusTotalService.CheckLinkInVirusTotalAsync(CheckLink, dbInstance);
            CheckLink=await _dmarc.CheckLinkByEasyDmarcAsync(CheckLink,dbInstance);
            int result = await _linksService.AddLinkDataInDb(dbInstance);
            return result;
        }
    }
}
