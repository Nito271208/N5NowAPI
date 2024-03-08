using MediatR;
using N5Now.Domain.DTOs;
using N5Now.Domain.Entities;

namespace N5Now.Infrastructure.Commands
{
    public class UpdatePermissionCommand : IRequest<PermissionDto>
    {
        public PermissionDto? UpdatePermission { get; set; }
    }
}
