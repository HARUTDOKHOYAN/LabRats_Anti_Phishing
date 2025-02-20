using AntiPhishingAPI.Data.Models;
using LearningASPweb.Data.Repositories;

namespace AntiPhishingAPI.Data.Repositories.Contracts
{
    public interface ICheckedLinkRepository: IRepository<DbData, int>
    {
        public Task<int> CreateAsync(DbData link);
        public Task<DbData>GetByUrlAsync(string url);
        
    }
}
