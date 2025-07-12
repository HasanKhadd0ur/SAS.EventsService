using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Topics.UseCases.Commands;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Domain.UserInterests.DomainErrors;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Regions.UseCases.Commands.UpdateTopic
{
    public class UpdateUserInterestCommandHandler : ICommandHandler<UpdateUserInterestCommand, Result>
    {
        private readonly IUserInterestsRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserInterestCommandHandler(IUserInterestsRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUserInterestCommand request, CancellationToken cancellationToken)
        {
            var topic = await _repo.GetByIdAsync(request.Id);
            
            if (topic is null) return Result.Invalid(UserInterestErrors.UserInterestUnExist);
            
            topic.UpdateName(request.InterestName);
            topic.UpdateInterestArea(request.radiusInKm);

            topic.UpdateLocation(_mapper.Map<Location>(request.Location));

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

}
