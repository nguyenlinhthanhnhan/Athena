using Athena.Application.DTOs.User;
using Athena.Core.Entities;

namespace Athena.Application.Identity;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    
    User GetById(int id);
    
    Task<User> RegisterAsync(RegisterRequest user);
}