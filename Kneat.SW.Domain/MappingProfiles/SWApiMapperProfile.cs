using AutoMapper;
using Kneat.SW.Domain.Entity;
using Kneat.SW.Domain.Infrastructure.Gateways.SWApi.Model;
using System.Collections.Generic;

namespace Kneat.SW.Domain.MappingProfiles
{
    public class SWApiMapperProfile : Profile
    {
        public SWApiMapperProfile()
        {
            CreateMap<StarshipApiModel, Starship>(MemberList.None).ReverseMap();
        }
    }
}