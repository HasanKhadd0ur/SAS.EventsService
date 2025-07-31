using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.NamedEntities.UseCases.Queries.GetAllNamedEntities;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers.NamedEntities
{
    public class NamedEntitiesController : APIController
    {
        private readonly IMediator _mediator;

        public NamedEntitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all named entities with optional pagination.
        /// </summary>
        /// <param name="pageNumber">Optional page number.</param>
        /// <param name="pageSize">Optional page size.</param>
        /// <returns>List of named entities.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllNamedEntities([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
            var query = new GetAllNamedEntitiesQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }
    }
}
