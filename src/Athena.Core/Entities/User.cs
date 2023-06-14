using Athena.Core.Common;
using Athena.Core.Common.Interfaces;

namespace Athena.Core.Entities;

public class User : FullAuditedEntity<int>
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public string? RefreshToken { get; set; }
}