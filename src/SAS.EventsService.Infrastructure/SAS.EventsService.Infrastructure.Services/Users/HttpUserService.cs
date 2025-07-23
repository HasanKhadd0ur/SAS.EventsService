using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SAS.EventsService.Application.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Infrastructure.Services.Users
{
    public class HttpUserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public HttpUserService(HttpClient httpClient,
                               IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync()
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Result.Unauthorized();

            return await GetUserByIdAsync(userId);
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/users/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDto>();
                    return user != null ? Result.Success(user) : Result.NotFound();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return Result.NotFound();

                return Result.Error($"Failed to fetch user: {response.StatusCode}");
            }
            catch (Exception ex)
            {
               return Result.Error("Internal server error");
            }
        }

        public async Task<Result<List<string>>> GetUserEmailsByIdsAsync(List<Guid> userIds)
        {
            var emails = new List<string>();

            foreach (var id in userIds)
            {
                var result = await GetUserByIdAsync(id);
                if (result.Status == ResultStatus.Ok)
                    emails.Add(result.Value.Email);
            }

            return Result.Success(emails);
        }
    }
}
