using TokenBroker.Domain.Responses;

namespace TokenBroker.Application.Interfaces;

public interface ITokenService
{
    Task<AccessTokenResponse> GetTokenAsync(string scope);
}