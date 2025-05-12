using SAS.EventsService.SharedKernel.DomainExceptions.Base;

namespace SAS.EventsService.SharedKernel.DomainExceptions
{
    public abstract class BadRequestException : DomainException
    {
        protected BadRequestException(string message)
            : base(message)
        {
        }
    }
}
