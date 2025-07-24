using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Application.Events.UseCases.Commands.AddMessageToEvent;
using SAS.EventsService.Application.Events.UseCases.Commands.BulkAddMessagesToEvent;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventInfo;
using SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventLocation;
using SAS.EventsService.Application.Events.UseCases.Queries.GetAllEvents;
using SAS.EventsService.Application.Events.UseCases.Queries.GetEventById;
using SAS.EventsService.Application.Events.UseCases.Queries.GetEventsByArea;
using SAS.EventsService.Application.Events.UseCases.Queries.GetEventsBySepcification;
using SAS.EventsService.Application.Events.UseCases.Queries.GetTodaySummary;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Presentation.Contracts.Events.Requests;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers
{
    public class EventsController : APIController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new event from detection system
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
        {
            var command = _mapper.Map<CreateEventFromDetectionCommand>(request);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }


        [HttpPost("{eventId}/message")]
        public async Task<IActionResult> AddMessageToEvent(Guid eventId, [FromBody] MessageDto message)
        {
            var command = new AddMessageToEventCommand(eventId, message);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPut("{eventId}/info")]
        public async Task<IActionResult> UpdateEventInfo(Guid eventId, [FromBody] EventInfo eventInfo)
        {
            var command = new UpdateEventInfoCommand(eventId, eventInfo);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Get events within a specified radius from a location
        /// </summary>
        [HttpGet("area")]
        public async Task<IActionResult> GetEventsByArea([FromQuery] GetEventsByLocationRadiusRequest request)
        {
            var query = _mapper.Map<GetEventsByLocationRadiusQuery>(request);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Retrieves all events from the system.
        /// </summary>
        /// <returns>A list of all events.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var query = new GetAllEventsQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the event.</param>
        /// <returns>The event corresponding to the provided ID.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEventByIdQuery(id));
            return HandleResult(result);
        }

        /// <summary>
        /// Get events by topic name.
        /// </summary>
        [HttpGet("by-topic")]
        public async Task<IActionResult> GetByTopic([FromQuery] string topic)
        {
            var spec = new EventsByTopicNameSpecification(topic);
            spec.ApplyOrderByDescending(e => e.CreatedAt);
            var result = await _mediator.Send(new GetEventsBySpecificationQuery(spec));
            return HandleResult(result);
        }

        /// <summary>
        /// Get events by region name.
        /// </summary>
        [HttpGet("by-region")]
        public async Task<IActionResult> GetByRegion([FromQuery] string region)
        {
            var spec = new EventsByRegionNameSpecification(region);
            var result = await _mediator.Send(new GetEventsBySpecificationQuery(spec));
            return HandleResult(result);
        }

        /// <summary>
        /// Get events updated after a specific date.
        /// </summary>
        [HttpGet("updated-after")]
        public async Task<IActionResult> GetByUpdatedAfter([FromQuery] DateTime lastUpdated)
        {
            var spec = new EventsByLastUpdatedAfterSpecification(lastUpdated);
            var result = await _mediator.Send(new GetEventsBySpecificationQuery(spec));
            return HandleResult(result);
        }

        /// <summary>
        /// Get events created between two dates.
        /// </summary>
        [HttpGet("created-between")]
        public async Task<IActionResult> GetByCreatedBetween([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var spec = new EventsByCreatedAtBetweenSpecification(from, to);
            var result = await _mediator.Send(new GetEventsBySpecificationQuery(spec));
            return HandleResult(result);
        }

        /// <summary>
        /// Get events created on a specific date.
        /// </summary>
        [HttpGet("by-date")]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        {
            var spec = new EventsByDateSpecification(date);
            var result = await _mediator.Send(new GetEventsBySpecificationQuery(spec));
            return HandleResult(result);
        }

        [HttpPost("{eventId}/messages/bulk")]
        public async Task<IActionResult> BulkAddMessagesToEvent(Guid eventId, [FromBody] List<MessageDto> messages)
        {
            var command = new BulkAddMessagesToEventCommand(eventId, messages);
            var result = await _mediator.Send(command);

            return HandleResult(result);

        }

        [HttpGet("{eventId}/messages")]
        public async Task<IActionResult> GetMessagesByEvent(Guid eventId)
        {
            var query = new GetEventMessagesQuery(eventId);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpGet("summary/today")]
        [ResponseCache(Duration = 600)]
        public async Task<IActionResult> GetTodaySummary()
        {
            var result = await _mediator.Send(new SummarizeTodayEventsQuery());
            return HandleResult(result);
        }

        [HttpPut("{eventId}/location")]
        public async Task<IActionResult> UpdateEventLocation(Guid eventId, [FromBody] UpdateEventLocationRequest request)
        {
            var command = _mapper.Map<UpdateEventLocationCommand>(request);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }


    }
}
