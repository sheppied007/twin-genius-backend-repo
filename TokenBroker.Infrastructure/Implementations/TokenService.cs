using TokenBroker.Domain.Responses;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using TokenBroker.Application.Interfaces;

namespace TokenBroker.Infrastructure.Implementations;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config) => _config = config;

    public async Task<AccessTokenResponse> GetTokenAsync(string scope)
    {
        var tenantId = _config["AzureAd:TenantId"];
        var clientId = _config["AzureAd:ClientId"];
        var clientSecret = _config["AzureAd:ClientSecret"];

        var app = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
            .Build();

        var result = await app.AcquireTokenForClient(new[] { scope }).ExecuteAsync();

        return new AccessTokenResponse(result.AccessToken, result.ExpiresOn.ToUnixTimeSeconds());
    }
}