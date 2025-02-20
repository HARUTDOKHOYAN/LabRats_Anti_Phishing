using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IPhishingChecker
    {
        public Task<DbData> CheckLinkPresenceInPhishingDbAsync(CheckingLink phishingLink);
    }
}
