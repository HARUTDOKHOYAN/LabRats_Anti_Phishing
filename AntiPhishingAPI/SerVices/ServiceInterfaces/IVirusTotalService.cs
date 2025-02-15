using AntiPhishingAPI.Data.DTO;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IVirusTotalService
    {
        public Task<CheckingLink> CheckLinkInVirusTotalAsync(CheckingLink link);
    }
}
