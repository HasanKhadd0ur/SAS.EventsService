﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SAS.EventsService.SharedKernel.Utilities
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        void ApplyOptionalPagination(int? pageSize, int? pageNumber);

    }
}
