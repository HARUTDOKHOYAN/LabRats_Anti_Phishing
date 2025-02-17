using AntiPhishingAPI.Data.DTO;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IPhishingChecker
    {
        public Task<CheckingLink> CheckLinkPresenceInPhishingDbAsync(CheckingLink phishingLink);
    }
}
