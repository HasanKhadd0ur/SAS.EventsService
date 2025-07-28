using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventInfo;
using SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventLocation;
using SAS.EventsService.Application.Events.UseCases.Queries.GetEventsByArea;
using SAS.EventsService.Application.Regions.UseCases.Commands.CreateTopic;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Presentation.Contracts.Events.Requests;
using SAS.EventsService.Presentation.Contracts.Topics.Requests;

namespace SAS.EventsService.Presentation.Mappers
{
    public class EventRequestToCommandProfile : Profile
    {
        public EventRequestToCommandProfile()
        {
            // CreateEventRequest ? CreateEventFromDetectionCommand
            CreateMap<CreateEventRequest, CreateEventFromDetectionCommand>();

            // UpdateEventInfoRequest ? UpdateEventInfoCommand
            CreateMap<UpdateEventInfoRequest, UpdateEventInfoCommand>();
            
            CreateMap<GetEventsByLocationRadiusRequest,GetEventsByLocationRadiusQuery>();
            CreateMap<UpdateEventLocationRequest, UpdateEventLocationCommand>();

        }
    }
    public class UserInterestsRequestToCommandProfile : Profile
    {
        public UserInterestsRequestToCommandProfile()
        {
            // CreateUserInterestRequest ? CreateUserInterestCommand
            CreateMap<CreateUserInterestRequest, CreateUserInterestCommand>();
            CreateMap<LocationDTO, Location>();
        }
    }
}
