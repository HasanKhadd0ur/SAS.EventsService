
using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.Application.Notifications.UseCases.Queries.GetNotificationByUser;
using SAS.EventsService.Domain.Notifications.DomainErrors;
using SAS.EventsService.Domain.Notifications.Entitties;
using SAS.EventsService.Domain.Notifications.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Specification;

namespace SAS.EventsService.Application.Notifications.UseCases.Queries.GetNotificationsByCurrentUser
{
    public class GetNotificationsByCurrentUserQueryHandler
       : IQueryHandler<GetNotificationsByCurrentUserQuery, Result<ICollection<NotificationDTO>>>
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetNotificationsByCurrentUserQueryHandler(INotificationRepository repository, IMapper mapper, ICurrentUserProvider currentUserProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<Result<ICollection<NotificationDTO>>> Handle(GetNotificationsByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Notification>(e => e.UserId == _currentUserProvider.UserId);

            spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

            var notifications = await _repository.ListAsync(spec);

            if (notifications is null || !notifications.Any())
                return Result.Invalid(NotificationErrors.UnExistUser);

            var dtos = _mapper.Map<ICollection<NotificationDTO>>(notifications);
            return Result.Success(dtos);
        }
    }
}
