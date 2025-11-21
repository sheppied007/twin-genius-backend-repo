using MediatR;
using TokenBroker.Application.Interfaces;
using TokenBroker.Application.Token.Queries;
using TokenBroker.Domain.Common;
using TokenBroker.Domain.Responses;

namespace TokenBroker.Application.Token.Handlers;

public class GetAdtTokenQueryHandler : IRequestHandler<GetAdtTokenQuery, BaseResult<AccessTokenResponse>>
{
    private readonly ITokenService _tokenService;

    public GetAdtTokenQueryHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<BaseResult<AccessTokenResponse>> Handle(GetAdtTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _tokenService.GetTokenAsync("https://digitaltwins.azure.net/.default");
            return BaseResult<AccessTokenResponse>.Ok(token);
        }
        catch (Exception ex)
        {
            return BaseResult<AccessTokenResponse>.Fail(ex.Message);
        }
    }
}