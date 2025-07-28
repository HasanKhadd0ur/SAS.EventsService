using Ardalis.Result;
using Microsoft.AspNetCore.Http;


namespace SAS.EventsService.Application.Contracts.Storage
{
    public interface IFileService
    {
        public Task<Result<string>> StoreFile(string fileName, IFormFile file);
        public Task<Result<IFormFile>> RetreiveFile(string fileUrl);
    }
}
