using TokenBroker.Domain.Responses;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using TokenBroker.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace TokenBroker.Infrastructure.Implementations;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;

    public TokenService(IConfiguration config, IMemoryCache cache)
    {
        _config = config;
        _cache = cache;
    }

    public async Task<AccessTokenResponse> GetTokenAsync(string scope)
    {
        var cacheKey = $"token:{scope}";

        if (_cache.TryGetValue(cacheKey, out AccessTokenResponse cachedToken))
        {
            // Avoid returning tokens close to expiration (e.g., 1 minute left)
            if (cachedToken != null && cachedToken.ExpiresIn > DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds())
            {
                return cachedToken;
            }
        }

        var tenantId = _config["AzureAd:TenantId"];
        var clientId = _config["AzureAd:ClientId"];
        var clientSecret = _config["AzureAd:ClientSecret"];

        var app = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
            .Build();

        var result = await app.AcquireTokenForClient(new[] { scope }).ExecuteAsync();

        var newToken = new AccessTokenResponse(result.AccessToken, result.ExpiresOn.ToUnixTimeSeconds());

        var expiry = result.ExpiresOn - TimeSpan.FromMinutes(1);

        _cache.Set(cacheKey, newToken, expiry);

        return newToken;
    }
}