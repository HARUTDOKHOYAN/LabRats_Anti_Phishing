using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IVirusTotalService
    {
        public Task<CheckingLink> CheckLinkInVirusTotalAsync(CheckingLink link, DbData instanceForDb);
    }
}
