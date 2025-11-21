namespace TokenBroker.Domain.Responses;

public record AccessTokenResponse(string AccessToken, long ExpiresIn);
