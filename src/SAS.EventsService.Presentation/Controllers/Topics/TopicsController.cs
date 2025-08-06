using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.Topics.UseCases.Commands;
using SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic;
using SAS.EventsService.Application.Topics.UseCases.Commands.DeleteTopic;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetTopicById;
using SAS.EventsService.Presentation.Contracts.Topics.Requests;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : APIController
    {
        private readonly IMediator _mediator;

        public TopicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new topic.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] CreateTopicRequest request)
        {
            var command = new CreateTopicCommand(request.Name, request.IconUrl, request.Description);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Get topic by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTopicById(Guid id)
        {
            var query = new GetTopicByIdQuery(id);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Get all topics.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var query = new GetAllTopicsQuery();
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Update an existing topic.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTopic(Guid id, [FromBody] CreateTopicRequest request)
        {
            var command = new UpdateTopicCommand(id, request.Name, request.IconUrl, request.Description);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Delete a topic by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTopic(Guid id)
        {
            var command = new DeleteTopicCommand(id);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
