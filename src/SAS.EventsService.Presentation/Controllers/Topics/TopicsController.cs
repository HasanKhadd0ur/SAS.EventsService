using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using SAS.EventsService.Presentation.Contracts.Topics.Requests;
using SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics;
using System.Threading.Tasks;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetTopicById;
using System;

namespace SAS.EventsService.Presentation.Controllers
{
    public class TopicsController : APIController
    {
        private readonly IMediator _mediator;

        public TopicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] CreateTopicRequest request)
        {
            var command = new CreateTopicCommand(request.Name,request.IconUrl,request.Description);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Get a topic by its unique identifier.
        /// </summary>
        /// <param name="id">The topic's GUID.</param>
        /// <returns>The topic details.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTopicById(Guid id)
        {
            var query = new GetTopicByIdQuery(id);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var query = new GetAllTopicsQuery();
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }
    }
}
