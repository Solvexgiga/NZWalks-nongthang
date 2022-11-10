using Microsoft.EntityFrameworkCore;
using NZWalksNongthang.API.data;
using NZWalksNongthang.API.Model.Domain;

namespace NZWalksNongthang.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
     

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}
