using AntiPhishingAPI.Data.DTO;
using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.Data.Repositories.Contracts;
using AntiPhishingAPI.SerVices.ServiceInterfaces;

namespace AntiPhishingAPI.SerVices.ServiceClasses
{
    public class LinkService : ILinksService
    {
        private readonly ICheckedLinkRepository _repository;

        public LinkService(ICheckedLinkRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> AddLinkDataInDb(DbData link)
        {
            int id = await _repository.CreateAsync(link);
            return id;
        }

        public async Task<DbData> GetCheckingLinkById(int id)
        {
            DbData result= await _repository.ReadAsync(id);
            return result;
        }
        public async Task<DbData> GetLinkDataByURLAsync(string url)
        {
            return await _repository.GetByUrlAsync(url);
        }
    }
}
