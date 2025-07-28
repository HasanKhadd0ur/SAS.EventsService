using Ardalis.Result;
using SAS.EventsService.Domain.NamedEntities.DomainErrors;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Commands.DeleteNamedEntity
{
    public class DeleteNamedEntityCommandHandler : ICommandHandler<DeleteNamedEntityCommand, Result>
    {
        private readonly INamedEntitiesRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNamedEntityCommandHandler(INamedEntitiesRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteNamedEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null)
                return Result.Invalid(NamedEntityErrors.UnExistNamedEntity);

            await _repo.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
