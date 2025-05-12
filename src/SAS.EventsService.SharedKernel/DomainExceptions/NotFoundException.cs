using SAS.EventsService.SharedKernel.DomainExceptions.Base;

namespace SAS.EventsService.SharedKernel.DomainExceptions
{
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string message)
            : base(message)
        {
        }
    }
}

