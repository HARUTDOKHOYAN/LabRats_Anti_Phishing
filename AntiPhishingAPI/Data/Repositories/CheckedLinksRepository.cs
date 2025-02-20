using AntiPhishingAPI.Data.Models;
using AntiPhishingAPI.Data.Repositories.Contracts;
using AutoMapper;
using LearningASPweb.Data;
using LearningASPweb.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntiPhishingAPI.Data.Repositories
{
    public class CheckedLinksRepository : AspTestDbRepositoryBase<DbData, int>, ICheckedLinkRepository
    {
        public CheckedLinksRepository(AnitPhoshingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        //hide the abstract class method not good
        public async Task<int> CreateAsync(DbData link)
        {
            await Context.AddAsync(link);
            await Context.SaveChangesAsync();
            return link.Id;
        }

        public async Task<DbData> GetByUrlAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            else
            {
               return await Context.Links.FirstOrDefaultAsync(l=>l.Url.Equals(url));
            }
        }
    }
}
