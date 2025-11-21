using MediatR;
using TokenBroker.Application.Interfaces;
using TokenBroker.Application.Token.Queries;
using TokenBroker.Domain.Common;
using TokenBroker.Domain.Responses;

namespace TokenBroker.Application.Token.Handlers;

public class GetStorageTokenQueryHandler : IRequestHandler<GetStorageTokenQuery, BaseResult<AccessTokenResponse>>
{
    private readonly ITokenService _tokenService;

    public GetStorageTokenQueryHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<BaseResult<AccessTokenResponse>> Handle(GetStorageTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _tokenService.GetTokenAsync("https://storage.azure.com/.default");
            return BaseResult<AccessTokenResponse>.Ok(token);
        }
        catch (Exception ex)
        {
            return BaseResult<AccessTokenResponse>.Fail($"Failed to get storage token: {ex.Message}");
        }
    }
}