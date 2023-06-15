using Athena.Application.DTOs.Authentication;
using Athena.Application.DTOs.User;
using Athena.Core.Entities;

namespace Athena.Application.Identity;

public interface IUserService
{
    User? GetById(int id);
    
    Task<User> RegisterAsync(RegisterRequest user);
}