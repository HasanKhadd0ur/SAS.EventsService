using Ardalis.Result;

namespace SAS.EventsService.SharedKernel.DomainErrors
{
    public class DomainError : ValidationError
    {
        public DomainError(string errorCode, string errorMessage) : base(errorCode,errorMessage)
        {
            ErrorCode = errorCode;
        }


    }
}
