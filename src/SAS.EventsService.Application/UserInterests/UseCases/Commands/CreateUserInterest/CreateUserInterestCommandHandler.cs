using Ardalis.Result;
using AutoMapper;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Repositories;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Regions.UseCases.Commands.CreateTopic
{
    public class CreateUserInterestCommandHandler : ICommandHandler<CreateUserInterestCommand, Result<Guid>>
    {
        private readonly IUserInterestsRepository _repo;
        private readonly IRepository<Location, Guid> _locationRepo ;
        private readonly IIdProvider _idProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserProvider _currentUserProvider;

        public CreateUserInterestCommandHandler(
                IUserInterestsRepository repo,
                IIdProvider idProvider,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ICurrentUserProvider currentUserProvider,
                IRepository<Location, Guid> locationRepo)
        {
            _repo = repo;
            _idProvider = idProvider;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
            _locationRepo = locationRepo;
        }

        public async Task<Result<Guid>> Handle(CreateUserInterestCommand request, CancellationToken cancellationToken)
        {
            // Map Location DTO to Location entity
            var location = _mapper.Map<Location>(request.Location);

            // Persist Location first to get its Id
            await _locationRepo.AddAsync(location);

            //await _unitOfWork.SaveChangesAsync();

            // create UserInterest and assign LocationId
            var interest = new UserInterest
            {
                Id = _idProvider.GenerateId<UserInterest>(),
                UserId = _currentUserProvider.UserId,
                InterestName = request.InterestName,
                RadiusInKm = request.RadiusInKm,
                Location = location
            };

            await _repo.AddAsync(interest);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(interest.Id);
        }

    }

}
