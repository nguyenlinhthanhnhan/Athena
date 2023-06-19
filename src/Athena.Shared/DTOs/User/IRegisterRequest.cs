namespace Athena.Shared.DTOs.User;

public interface IRegisterRequest
{
    string Email { get; set; }

    string Password { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }
}