using AutoMapper;
using LiveTogether.Models.Dto;

namespace LiveTogether.Models.MapProfiles
{
    public class WarrantyProfile : Profile
    {
        public WarrantyProfile()
        {
            CreateMap<Warranty, WarrantyDto>();
        }
    }
}