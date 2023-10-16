using CharchoobApi.Application.Common.Models;
using CharchoobApi.Infrastructure.Identity;

namespace CharchoobApi.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<bool> CreateUserAsync(string username, string email,  string password);
    Task<Tokens> GenerateTokensAsync(ApplicationUser user);
}
