using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.Application.Notifications.UseCases.Commands.AddEventNotificationCommand;
using SAS.EventsService.Application.Notifications.UseCases.Queries.GetNotificationsByCurrentUser;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers.Notifications
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : APIController
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetCurrentUserNotifications([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var command = new GetNotificationsByCurrentUserQuery(pageNumber,pageSize);
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }
    }
}
