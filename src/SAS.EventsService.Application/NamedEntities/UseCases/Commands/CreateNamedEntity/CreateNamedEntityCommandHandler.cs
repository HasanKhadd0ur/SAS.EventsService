using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.EventsService.Application.NamedEntities.UseCases.Commands.CreateNamedEntity;
using SAS.EventsService.Domain.NamedEntities.DomainErrors;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Commands
{
    public class CreateNamedEntityCommandHandler
        : ICommandHandler<CreateNamedEntityCommand, Result<Guid>>
    {
        private readonly INamedEntitiesRepository _entitiesRepo;
        private readonly INamedEntityTypesRepository _typesRepo;
        private readonly IIdProvider _idProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateProvider;

        public CreateNamedEntityCommandHandler(
            INamedEntitiesRepository entitiesRepo,
            INamedEntityTypesRepository typesRepo,
            IIdProvider idProvider,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeProvider dateProvider)
        {
            _entitiesRepo = entitiesRepo;
            _typesRepo = typesRepo;
            _idProvider = idProvider;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateProvider = dateProvider;
        }

        public async Task<Result<Guid>> Handle(CreateNamedEntityCommand request, CancellationToken cancellationToken)
        {
            // Check if the NamedEntityType exists
            var type = await _typesRepo.GetByIdAsync(request.TypeId);
            if (type is null)
                return Result.Invalid(NamedEntityErrors.UnExistNamedEntityType);

            var entity = new NamedEntity
            {
                Id = _idProvider.GenerateId<NamedEntity>(),
                EntityName = request.EntityName,
                TypeId = request.TypeId,
                Type = type,
                LastMentionedAt=_dateProvider.UtcNow
            };

            await _entitiesRepo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(entity.Id );
        }
    }
}
