using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.Regions.UseCases.Commands.CreateTopic;
using SAS.EventsService.Application.Regions.UseCases.Commands.DeleteTopic;
using SAS.EventsService.Application.Regions.UseCases.Queries.GetAllTopics;
using SAS.EventsService.Application.Regions.UseCases.Queries.GetTopicById;
using SAS.EventsService.Presentation.Contracts.Topics.Requests;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers
{
    public class UserInterestsController : APIController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserInterestsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserIterest([FromBody] CreateUserInterestRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateUserInterestCommand>(request));
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserInterestById(Guid id)
        {
            var query = new GetUserInterestByIdQuery(id);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserInterest(Guid id)
        {
            var query = new DeleteUserInterestCommand(id);
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
