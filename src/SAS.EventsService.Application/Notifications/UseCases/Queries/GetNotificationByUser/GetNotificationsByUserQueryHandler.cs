
using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.Domain.Notifications.DomainErrors;
using SAS.EventsService.Domain.Notifications.Entitties;
using SAS.EventsService.Domain.Notifications.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Specification;

namespace SAS.EventsService.Application.Notifications.UseCases.Queries.GetNotificationByUser
{
    public class GetNotificationsByUserQueryHandler
       : IQueryHandler<GetNotificationsByUserQuery, Result<ICollection<NotificationDTO>>>
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public GetNotificationsByUserQueryHandler(INotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<NotificationDTO>>> Handle(GetNotificationsByUserQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Notification>(e => e.UserId == request.UserId);
            spec.ApplyOptionalPagination(request.PageSize,request.PageNumber);
            var notifications = await _repository.ListAsync(spec);

            if (notifications is null || !notifications.Any())
                return Result.Invalid(NotificationErrors.UnExistUser);

            var dtos = _mapper.Map<ICollection<NotificationDTO>>(notifications);
            return Result.Success(dtos);
        }
    }
}
