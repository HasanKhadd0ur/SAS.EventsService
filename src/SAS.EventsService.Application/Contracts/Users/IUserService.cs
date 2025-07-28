

using Ardalis.Result;

namespace SAS.EventsService.Application.Contracts.Users
{
    public interface IUserService
    {
        Task<Result<UserDto>> GetCurrentUserAsync();
        Task<Result<UserDto>> GetUserByIdAsync(Guid userId);
        Task<Result<List<string>>> GetUserEmailsByIdsAsync(List<Guid> userIds);
    }

}
