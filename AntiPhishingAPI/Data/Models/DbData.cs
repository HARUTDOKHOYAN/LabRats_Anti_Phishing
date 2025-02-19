using AntiPhishingAPI.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace AntiPhishingAPI.Data.Models
{
    public class DbData
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public double Dangerousity { get; set; }   
        public bool IsLinkActive { get; set; }
        public bool IsLinkInPhishingBlackList { get; set; }
        public VirusTotalResultDTO VirusTotalResult { get; set; }
        public EasyDmarcResponseDto EasyDmarcResponse { get; set; } 
    }
}
