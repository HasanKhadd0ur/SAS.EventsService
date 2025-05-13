using SAS.EventsService.Application.Contracts.Providers;
using System.Security.Cryptography;
using System.Text;

namespace SAS.EventsService.Infrastructure.Services
{
    public class IdProvider : IIdProvider
    {
        public Guid GenerateId<T>(string uniqueKey)
        {
            var input = $"{typeof(T).Name}:{uniqueKey.Trim().ToLowerInvariant()}";
            return GenerateGuidFromString(input);
        }

        public Guid GenerateId<T>(params object[] keyParts)
        {
            var combinedKey = string.Join(":", keyParts.Select(k => k?.ToString()?.Trim().ToLowerInvariant()));
            var input = $"{typeof(T).Name}:{combinedKey}";
            return GenerateGuidFromString(input);
        }

        public Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }

        private Guid GenerateGuidFromString(string input)
        {
            using var provider = MD5.Create(); // SHA256 is stronger but MD5 fits 16 bytes
            byte[] hash = provider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return new Guid(hash[..16]);
        }
    }
}
