using AntiPhishingAPI.Data.DTO;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface IEasyDmarc
    {
        public Task<CheckingLink> CheckLinkByEasyDmarcAsync(CheckingLink link);
    }
}
