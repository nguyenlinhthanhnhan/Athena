using Athena.Application.Commons.Exceptions;
using Athena.Application.DTOs.User;
using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Athena.Application.Identity.Services;

public class UserService : IUserService
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        
        var passwordHasher = new PasswordHasher<User>();
        newUser.Password = passwordHasher.HashPassword(newUser, user.Password);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public User? GetById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
    
}