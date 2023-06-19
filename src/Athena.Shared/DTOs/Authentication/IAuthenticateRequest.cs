namespace Athena.Shared.DTOs.Authentication;

public interface IAuthenticateRequest
{
    string Email { get; set; }

    string Password { get; set; }
}