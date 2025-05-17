using Ardalis.Result;
using SAS.EventsService.Application.Topics.UseCases.Commands.DeleteTopic;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Topics.UseCases.Commands
{
    public class DeleteTopicCommandHandler : ICommandHandler<DeleteTopicCommand,Result>
    {
        private readonly ITopicsRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTopicCommandHandler(ITopicsRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await _repo.GetByIdAsync(request.Id);

            if (topic is null) return Result.Invalid(TopicErrors.UnExistTopic);
            
            await _repo.DeleteAsync(topic);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }

}
