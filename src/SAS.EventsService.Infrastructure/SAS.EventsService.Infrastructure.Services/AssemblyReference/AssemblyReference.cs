using System.Reflection;

namespace SAS.EventsService.Infrastructure.Services
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    }
}
