using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IEasyDmarc
    {
        public Task<CheckingLink> CheckLinkByEasyDmarcAsync(CheckingLink link, DbData instanceForDb);
    }
}
