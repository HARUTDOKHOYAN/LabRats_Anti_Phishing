using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.Data.Repositories.Contracts;
using AutoMapper;
using LearningASPweb.Data;
using LearningASPweb.Data.Repositories;

namespace AntiPhishingAPI.Data.Repositories
{
    public class CheckedLinksRepository : AspTestDbRepositoryBase<CheckedLink, int>, ICheckedLinkRepository
    {
        public CheckedLinksRepository(AnitPhoshingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        //hide the abstract class method not good
        public async Task<int> CreateAsync(CheckedLink link)
        {
            await Context.AddAsync(link);
            await Context.SaveChangesAsync();
            return link.Id;
        }
    
    }
}
