using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.Regions.UseCases.Commands.CreateTopic;
using SAS.EventsService.Application.Regions.UseCases.Queries.GetAllTopics;
using SAS.EventsService.Application.Regions.UseCases.Queries.GetTopicById;
using SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetTopicById;
using SAS.EventsService.Presentation.Contracts.Topics.Requests;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers
{
    public class UserInterestsController : APIController
    {
        private readonly IMediator _mediator;

        public UserInterestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserIterest([FromBody] CreateUserInterestCommand request)
        {
            var result = await _mediator.Send(request);
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserInterestById(Guid id)
        {
            var query = new GetUserInterestByIdQuery(id);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterests()
        {
            var query = new GetAllInterestsQuery();
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }
    }
}
