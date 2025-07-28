using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Queries.GetAllNamedEntities
{
    public class GetAllNamedEntitiesQueryHandler
            : IQueryHandler<GetAllNamedEntitiesQuery, Result<ICollection<NamedEntityDto>>>
    {
        private readonly INamedEntitiesRepository _repository;
        private readonly IMapper _mapper;

        public GetAllNamedEntitiesQueryHandler(INamedEntitiesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<NamedEntityDto>>> Handle(GetAllNamedEntitiesQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<NamedEntity>();
            spec.ApplyOptionalPagination(request.PageSize, request.PageNumber);

            var entities = await _repository.ListAsync(spec);
            var result = _mapper.Map<ICollection<NamedEntityDto>>(entities);

            return Result.Success(result);
        }
    }
}
