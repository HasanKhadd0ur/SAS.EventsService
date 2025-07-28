using Ardalis.Result;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.ChangeEventTopic
{
    public class ChangeEventTopicCommandHandler : ICommandHandler<ChangeEventTopicCommand, Result>
    {
        private readonly IEventsRepository _eventRepo;
        private readonly ITopicsRepository _topicsRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeEventTopicCommandHandler(
            IEventsRepository eventRepo,
            ITopicsRepository topicsRepo,
            IUnitOfWork unitOfWork)
        {
            _eventRepo = eventRepo;
            _topicsRepo = topicsRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeEventTopicCommand request, CancellationToken cancellationToken)
        {
            var ev = await _eventRepo.GetByIdAsync(request.EventId);
            if (ev is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var topic = await _topicsRepo.GetByIdAsync(request.TopicId);
            if (topic is null)
                return Result.Invalid(TopicErrors.UnExistTopic);

            ev.ChangeTopic(topic);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
