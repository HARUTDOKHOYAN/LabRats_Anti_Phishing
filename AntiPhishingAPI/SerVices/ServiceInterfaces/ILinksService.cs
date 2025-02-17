using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface ILinksService
    {
        Task<int> AddLinkDataInDb(CheckedLink link);
        Task<CheckedLink>GetCheckingLinkById(int id);
    }
}
