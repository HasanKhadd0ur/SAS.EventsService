using SAS.SharedKernel.DomainExceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Domain.Events.DomainExceptions
{
    public static class EventExceptions
    {
        public static DomainException MessageNull()
        {
            return new DomainException("Message should not be null");
        }

        public static DomainException NamedEntityNull()
        {
            return new DomainException("NamedEntity cannot be null");
        }

        public static DomainException LocationNull()
        {
            return new DomainException("Location must not be null");
        }
        public static DomainException TopicNull()
        {
            return new DomainException("Topic must not be null");
        }
    }
}
