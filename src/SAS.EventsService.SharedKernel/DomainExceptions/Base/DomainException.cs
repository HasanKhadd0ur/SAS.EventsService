using System;

namespace SAS.EventsService.SharedKernel.DomainExceptions.Base
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
