using Athena.Shared.DTOs.Authentication;

namespace Athena.Application.DTOs.Authentication;

public class RefreshTokenRequest : IRefreshTokenRequest
{
    public string AccessToken { get; set; } = null!;
}