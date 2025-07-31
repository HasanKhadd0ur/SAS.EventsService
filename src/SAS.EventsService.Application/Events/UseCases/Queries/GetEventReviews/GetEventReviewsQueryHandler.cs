using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventReviews
{
    public class GetEventReviewsQueryHandler : IQueryHandler<GetEventReviewsQuery, Result<List<ReviewDto>>>
    {
        private readonly IRepository<Review, Guid> _reviewsRepository;

        public GetEventReviewsQueryHandler(IRepository<Review, Guid> reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        public async Task<Result<List<ReviewDto>>> Handle(GetEventReviewsQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Review>(e => e.EventId == request.EventId);
            var reviews = await _reviewsRepository.ListAsync(spec);

            var dtoList = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                EventId = r.EventId,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                LastUpdatedAt = r.LastUpdatedAt
            }).ToList();

            return Result.Success(dtoList);
        }
    }
}
