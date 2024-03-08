using MediatR;
using N5Now.Domain.DTOs;
using N5Now.Domain.Entities;

namespace EvaluacionNetInfrastructure.Queries
{
    public class GetPermissionQuery : IRequest<PermissionTypeDto>;
}
