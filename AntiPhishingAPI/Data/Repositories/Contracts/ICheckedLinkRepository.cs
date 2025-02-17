using AntiPhishingAPI.Data.Models;
using LearningASPweb.Data.Repositories;

namespace AntiPhishingAPI.Data.Repositories.Contracts
{
    public interface ICheckedLinkRepository: IRepository<CheckedLink, int>
    {
        public Task<int> CreateAsync(CheckedLink link);
        
    }
}
