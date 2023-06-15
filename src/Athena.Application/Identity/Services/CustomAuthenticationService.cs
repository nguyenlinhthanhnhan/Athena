using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Athena.Application.Commons.Exceptions;
using Athena.Application.DTOs.Authentication;
using Athena.Application.DTOs.User;
using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Athena.Application.Identity.Services;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly AuthSettings _authSettings;
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public CustomAuthenticationService(IOptions<AuthSettings> appSettings, AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _authSettings = appSettings.Value;
    }

    // Login
    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);

        if (user == null)
        {
            throw new BadRequestException("Email or password is incorrect");
        }

        // Check password
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Email or password is incorrect");
        }

        var token = GenerateJwtToken(user);

        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;

        user.LastLogin = DateTime.UtcNow;

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        return new AuthenticateResponse(token, refreshToken);
    }

    // Revoke
    public void RevokeToken(int userId)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
            throw new NotFoundException($"No user found with id {userId}");

        user.RefreshToken = null;
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
    }

    // Refresh Token
    public AuthenticateResponse RefreshToken(int userId, string token)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == userId);

        // If user is null or refresh token is not equal to token or refresh token is null
        if (user == null || user.RefreshToken != token || user.RefreshToken == null)
            throw new BadRequestException("Invalid token");

        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        var newAccessToken = GenerateJwtToken(user);

        return new AuthenticateResponse(newAccessToken, newRefreshToken);
    }

    // Generate Access Token
    private string GenerateJwtToken(User user)
    {
        byte[] key = Encoding.ASCII.GetBytes(_authSettings.AccessTokenSecret);
        var accessTokenExpiration = _authSettings.AccessTokenExpiration;

        // Convert time get from appsettings.json to seconds
        var expires = ConvertTimeToSeconds(accessTokenExpiration);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject =
                new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }),
            Expires = DateTime.UtcNow.AddSeconds(expires),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    // Generate refresh token, has default expiry of 7 days
    private string GenerateRefreshToken()
    {
        var key = Encoding.ASCII.GetBytes(_authSettings.RefreshTokenSecret);

        var refreshTokenExpiration = _authSettings.RefreshTokenExpiration;

        var expires = ConvertTimeToSeconds(refreshTokenExpiration);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddSeconds(expires),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Convert time get from appsettings.json to seconds
    /// </summary>
    /// <param name="time">Time from appsettings.json (ex: 1d, 5m...)</param>
    /// <returns></returns>
    private static int ConvertTimeToSeconds(string time)
    {
        return time switch
        {
            "1m" => 60,
            "5m" => 300,
            "10m" => 600,
            "15m" => 900,
            "30m" => 1800,
            "1h" => 3600,
            "2h" => 7200,
            "6h" => 21600,
            "12h" => 43200,
            "1d" => 86400,
            "2d" => 172800,
            "3d" => 259200,
            "4d" => 345600,
            "5d" => 432000,
            "6d" => 518400,
            "7d" => 604800,
            _ => 604800
        };
    }
}