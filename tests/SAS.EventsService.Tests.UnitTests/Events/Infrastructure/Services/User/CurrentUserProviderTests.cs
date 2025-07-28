using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using SAS.EventsService.Infrastructure.Services.Providers;
using System.Security.Claims;

namespace SAS.EventsService.Tests.UnitTests.Events.Infrastructure.Services.User
{
    public class CurrentUserProviderTests
    {
        [Fact]
        public void UserId_ReturnsGuidFromNameIdentifierClaim()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.User).Returns(principal);

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var provider = new CurrentUserProvider(accessorMock.Object);

            // Act
            var result = provider.UserId;

            // Assert
            result.Should().Be(userId);
        }

        [Fact]
        public void UserId_ReturnsEmptyGuid_WhenNoUser()
        {
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns((HttpContext)null);

            var provider = new CurrentUserProvider(accessorMock.Object);

            provider.UserId.Should().Be(Guid.Empty);
        }

        [Fact]
        public void Email_ReturnsEmailClaimValue()
        {
            var email = "test@example.com";
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.User).Returns(principal);

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var provider = new CurrentUserProvider(accessorMock.Object);

            provider.Email.Should().Be(email);
        }

        [Fact]
        public void Roles_ReturnsAllRoleClaims()
        {
            var roles = new[] { "Admin", "User" };
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, roles[0]),
            new Claim(ClaimTypes.Role, roles[1])
        };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.User).Returns(principal);

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var provider = new CurrentUserProvider(accessorMock.Object);

            provider.Roles.Should().BeEquivalentTo(roles);
        }
    }
}
