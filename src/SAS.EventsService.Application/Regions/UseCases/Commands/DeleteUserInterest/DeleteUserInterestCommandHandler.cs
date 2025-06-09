using Ardalis.Result;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Regions.UseCases.Commands.DeleteTopic;
using SAS.EventsService.Domain.Regions.DomainErrors;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Topics.UseCases.Commands
{
    public class DeleteUserInterestCommandHandler : ICommandHandler<DeleteUserInterestCommand, Result>
    {
        private readonly IUserInterestsRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserInterestCommandHandler(IUserInterestsRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteUserInterestCommand request, CancellationToken cancellationToken)
        {
            var interest = await _repo.GetByIdAsync(request.Id);

            if (interest is null) return Result.Invalid(UserInterestErrors.UserInterestUnExist);
            
            await _repo.DeleteAsync(interest);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }

}
