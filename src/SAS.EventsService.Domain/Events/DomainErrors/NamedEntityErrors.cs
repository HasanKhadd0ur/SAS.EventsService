using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Common.Errors;

public static class NamedEntityErrors
{
    public static readonly DomainError UnExistNamedEntity = 
        new DomainError("NamedEntity.NotFound", "Named entity not found");

    public static readonly DomainError UnExistNamedEntityType =
        new DomainError("NamedEntityType.NotFound", "Named entity type not found");
}