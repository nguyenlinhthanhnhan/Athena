using Athena.Application.Commons.Mappings;
using Athena.Shared.DTOs.User;

namespace Athena.Application.DTOs.User;

public class RegisterRequest : IRegisterRequest, IMapTo<Core.Entities.User>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}