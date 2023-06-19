using System.ComponentModel.DataAnnotations;
using Athena.Shared.DTOs.Authentication;

namespace Athena.Application.DTOs.Authentication;

public class AuthenticateRequest : IAuthenticateRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}