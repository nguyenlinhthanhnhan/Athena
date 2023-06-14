using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Athena.Application.Commons.Exceptions;
using Athena.Application.DTOs.User;
using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Athena.Application.Identity.Services;

public class UserService : IUserService
{
    private readonly AuthSettings _authSettings;
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IOptions<AuthSettings> appSettings, AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _authSettings = appSettings.Value;
    }
    
    // Create user
    public async Task<User> RegisterAsync(RegisterRequest user)
    {
        // validation
        if (string.IsNullOrWhiteSpace(user.Password))
            throw new BadRequestException("Password is required");

        if (_context.Users.Any(x => x.Email == user.Email))
            throw new BadRequestException("Email \"" + user.Email + "\" is already taken");

        var newUser = _mapper.Map<User>(user);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }


    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        if (user == null)
            return null;

        var token = GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }


    public User? GetById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

    private string GenerateJwtToken(User user)
    {
        byte[] key = Encoding.ASCII.GetBytes(_authSettings.AccessTokenSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject =
                new ClaimsIdentity(new[] { new Claim("sub", user.Id.ToString()), new Claim("email", user.Email) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}