using AutoMapper;

namespace NZWalksNongthang.API.Profiles
{
    public class RegionsProfile :Profile
    {
        public RegionsProfile()
        {
            CreateMap<Model.Domain.Region, Model.DTO.Region>()
                .ReverseMap();
        }
    }
}
