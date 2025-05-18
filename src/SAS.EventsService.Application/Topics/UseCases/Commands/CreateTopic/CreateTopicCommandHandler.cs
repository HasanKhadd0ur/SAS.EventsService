using Ardalis.Result;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic
{
    public class CreateTopicCommandHandler : ICommandHandler<CreateTopicCommand, Result<Guid>>
    {
        private readonly ITopicsRepository _repo;
        private readonly IIdProvider _idProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTopicCommandHandler(ITopicsRepository repo, IIdProvider idProvider, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _idProvider = idProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Topic { Id = _idProvider.GenerateId<Topic>(), Name = request.Name };
            await _repo.AddAsync(topic);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(topic.Id);
        }
    }

}
