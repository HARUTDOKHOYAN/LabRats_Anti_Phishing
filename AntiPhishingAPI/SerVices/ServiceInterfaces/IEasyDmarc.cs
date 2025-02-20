using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IEasyDmarc
    {
        public Task<DbData> CheckLinkByEasyDmarcAsync(CheckingLink link);
    }
}
