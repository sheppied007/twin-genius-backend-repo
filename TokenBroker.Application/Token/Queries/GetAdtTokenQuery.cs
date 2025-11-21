using MediatR;
using TokenBroker.Domain.Common;
using TokenBroker.Domain.Responses;

namespace TokenBroker.Application.Token.Queries;

public record GetAdtTokenQuery : IRequest<BaseResult<AccessTokenResponse>>;