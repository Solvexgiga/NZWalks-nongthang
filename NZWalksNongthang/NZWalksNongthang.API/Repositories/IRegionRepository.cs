using NZWalksNongthang.API.Model.Domain;

namespace NZWalksNongthang.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
