using MediatR;
using N5Now.Domain.DTOs;


namespace N5Now.Infrastructure.Commands
{
    public class CreatePermissionCommand : IRequest<PermissionDto>
    {
        public PermissionDto? CreatePermission { get; set; }
    }
}
