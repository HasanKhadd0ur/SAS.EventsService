﻿using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Regions
{
    public class UserInterestsRepository : BaseRepository<UserInterest, Guid>, IUserInterestsRepository
    {
        public UserInterestsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }

}
