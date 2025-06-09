using Ardalis.Result;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Topics.UseCases.Commands
{
    public class UpdateTopicCommandHandler : ICommandHandler<UpdateTopicCommand,Result>
    {
        private readonly ITopicsRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTopicCommandHandler(ITopicsRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await _repo.GetByIdAsync(request.Id);
            
            if (topic is null) return Result.Invalid(TopicErrors.UnExistTopic);
            
            topic.UpdateName(request.Name);
            topic.UpdateDescription(request.Description);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

}
