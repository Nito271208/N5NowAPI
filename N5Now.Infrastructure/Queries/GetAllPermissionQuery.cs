using MediatR;
using N5Now.Domain.DTOs;

namespace EvaluacionNetInfrastructure.Queries
{
    public class GetAllPermissionQuery : IRequest<PermissionDto>;
}
