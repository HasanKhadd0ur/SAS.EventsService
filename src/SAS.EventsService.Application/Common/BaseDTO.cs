using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Application.Common
{
    public class BaseDTO<T>
    {
        public T Id { get; set; }
    }
    public class PaginatedDTO<T> : BaseDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
