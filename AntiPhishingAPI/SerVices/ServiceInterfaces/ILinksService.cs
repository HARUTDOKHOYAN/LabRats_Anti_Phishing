using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;

namespace AntiPhishingAPI.SerVices.ServiceInterfaces
{
    public interface ILinksService
    {
        Task<int> AddLinkDataInDb(DbData link);
        Task<DbData>GetCheckingLinkById(int id);
        Task<DbData> GetLinkDataByURLAsync(string url);
    }
}
