using System.Reflection;

namespace SAS.EventsService.Infrastructure.Persistence
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    }
}
