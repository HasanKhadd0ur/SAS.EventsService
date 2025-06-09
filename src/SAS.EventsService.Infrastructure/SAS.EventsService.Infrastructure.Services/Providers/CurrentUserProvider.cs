using SAS.EventsService.Application.Contracts.Providers;

namespace SAS.EventsService.Infrastructure.Services.Providers
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public Guid UserId => Guid.NewGuid();

        public string Email => "hasan.b.khaddour@gmail.mail";

        public IEnumerable<string> Roles => [];
    }
}
