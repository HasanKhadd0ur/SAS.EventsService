using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using SAS.EventsService.Presentation.Contracts.Topics.Requests;
using SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics;
using System.Threading.Tasks;

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
            var command = new CreateTopicCommand(request.Name);
            var result = await _mediator.Send(command);
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
