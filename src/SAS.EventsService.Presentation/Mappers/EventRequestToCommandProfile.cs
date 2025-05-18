using AutoMapper;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventInfo;
using SAS.EventsService.Application.Events.UseCases.Queries.GetEventsByArea;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Presentation.Contracts.Events.Requests;

namespace SAS.EventsService.Presentation.Mappers
{
    public class EventRequestToCommandProfile : Profile
    {
        public EventRequestToCommandProfile()
        {
            // CreateEventRequest → CreateEventFromDetectionCommand
            CreateMap<CreateEventRequest, CreateEventFromDetectionCommand>();

            // UpdateEventInfoRequest → UpdateEventInfoCommand
            CreateMap<UpdateEventInfoRequest, UpdateEventInfoCommand>();
            
            CreateMap<GetEventsByLocationRadiusRequest,GetEventsByLocationRadiusQuery>();
        }
    }
}
