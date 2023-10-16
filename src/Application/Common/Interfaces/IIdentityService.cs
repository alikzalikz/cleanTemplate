using CharchoobApi.Application.Common.Models;

namespace CharchoobApi.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<bool> CreateUserAsync(string userName, string email,  string password);
}
