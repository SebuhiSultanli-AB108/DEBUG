using AutoMapper;
using DEBUG.BL.DTOs.ReportItemDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Profiles;

public class ReportItemProfile : Profile
{
    public ReportItemProfile()
    {
        CreateMap<ReportItemCreateDTO, ReportItem>();
        CreateMap<ReportItemGetDTO, ReportItem>()
            .ReverseMap();
    }
}