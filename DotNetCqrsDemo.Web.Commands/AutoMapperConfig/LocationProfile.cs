using AutoMapper;
using DotNetCqrsDemo.Domain.Commands;
using DotNetCqrsDemo.Domain.Events;
using DotNetCqrsDemo.Domain.ReadModel;
using DotNetCqrsDemo.Web.Commands.Requests.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Web.Commands.AutoMapperConfig
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<CreateLocationRequest, CreateLocationCommand>()
                .ConstructUsing(x => new CreateLocationCommand(Guid.NewGuid(), x.LocationID, x.StreetAddress, x.City, x.State, x.PostalCode));

            CreateMap<LocationCreatedEvent, LocationRM>()
                .ForMember(dest => dest.AggregateID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
