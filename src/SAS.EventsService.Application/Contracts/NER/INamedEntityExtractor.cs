using SAS.EventsService.Application.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Application.Contracts.NER
{
    public interface INamedEntityExtractor
    {
        Task<List<NamedEntityDto>> Extract(string text);
    }
}
