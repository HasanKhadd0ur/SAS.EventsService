using AutoMapper;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Application.Regions.Common;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.Regions.Entities;

namespace SAS.EventsService.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            // Map command to Location entity (new or update)
            CreateMap<CreateEventFromDetectionCommand, Location>()
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CountryName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CityName));

            // Map command to Event aggregate (partial mapping)
            CreateMap<CreateEventFromDetectionCommand, Event>()
                // EventInfo is a nested value object
                .ForMember(dest => dest.EventInfo, opt => opt.MapFrom(src => src))
                // Topic, Region and Location fks are set in handler, so ignore
                .ForMember(dest => dest.Topic, opt => opt.Ignore())
                .ForMember(dest => dest.Region, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Messages, opt => opt.Ignore())
                // Timestamps will be set by the constructor or after mapping
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());

                CreateMap<CreateEventFromDetectionCommand, EventInfo>()
                 .ConstructUsing(src => src.EventInfo);

                CreateMap<Event, EventDTO>();
            
                CreateMap<Location, LocationDTO>();
                CreateMap<LocationDTO, Location>();

                CreateMap<Message, MessageDto>();
                CreateMap<MessageDto, Message>()
                    .ForMember(dest => dest.EventId, opt => opt.Ignore());


        }
    }
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic, TopicDTO>();
        }
    }
    public class UserInterestProfile : Profile
    {
        public UserInterestProfile()
        {
            CreateMap<UserInterest, UserInterestDto>().ReverseMap();

        }
    }


}
