using AutoMapper;
using MaritimeManager.App.DTOs;
using MaritimeManager.Domain.Entities;

namespace MaritimeManager.App.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PointOfInterest, PointOfInterestDto>();
        CreateMap<CreatePointOfInterestDto, PointOfInterest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Identifier, opt => opt.Ignore());
        CreateMap<UpdatePointOfInterestDto, PointOfInterest>();
    }
}