using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CharchoobApi.Application.Common.Interfaces;
using CharchoobApi.Application.Common.Models;
using CharchoobApi.Domain.Common;
using CharchoobApi.Domain.Entities.gnr;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CharchoobApi.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IApplicationDbContext dbContext,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<bool> CreateUserAsync(string username, string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = username,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            if (!_roleManager.RoleExistsAsync(Sd.Admin).GetAwaiter().GetResult())
            {
                // create role in database
                await _roleManager.CreateAsync(new IdentityRole(Sd.User));
                await _roleManager.CreateAsync(new IdentityRole(Sd.Admin));
            }

            await _userManager.AddToRoleAsync(user, Sd.User);
            return true;
        }

        return false;
    }

    public async Task<Tokens> GenerateTokensAsync(ApplicationUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AuthOptions:SecureKey").Value!);

        int accessLifetime = int.Parse(_configuration.GetSection("AuthOptions:AccessTokenLife").Value!);
        int refreshTokenLifetime = int.Parse(_configuration.GetSection("AuthOptions:RefreshTokenLife").Value!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, Sd.User),
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),

            Expires = DateTime.UtcNow.AddMinutes(accessLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var accessToken = jwtTokenHandler.WriteToken(token);

        var refreshToken = new TblRefreshToken()
        {
            JwtId = token.Id,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            UserId = user.Id,
            CreateDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(refreshTokenLifetime)
        };

        await _dbContext.TblRefreshToken.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return new Tokens()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        };
    }
}