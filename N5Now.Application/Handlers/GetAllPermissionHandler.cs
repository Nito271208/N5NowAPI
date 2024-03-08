using EvaluacionNetInfrastructure.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N5Now.Domain.DTOs;
using N5Now.Infrastructure.Commons;
using N5Now.Infrastructure.Persistences.Contexts;
using N5Now.Utilities.Static;
using Nest;

namespace N5Now.Application.Handlers
{
    public class GetAllPermissionHandler : IRequestHandler<GetAllPermissionQuery, PermissionDto>
    {
        #region Privates Variables

        private readonly DbApiContext _dbContext;

        #endregion

        #region Constructor

        public GetAllPermissionHandler(DbApiContext context)
        {
            _dbContext = context;
        }

        #endregion

        #region Principal Method Get Permissions Handler
        public async Task<PermissionDto> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Realizamos la búsqueda de todos los permisos.
                var permissionItems = await _dbContext.Permissions
                                                                  .Where(p => p.Id != 0) // Exclude entries with id equal to 0
                                                                  .ToListAsync();

                if (permissionItems == null || !permissionItems.Any())
                {
                    return new PermissionDto { Message = ReplyMessages.MESSAGE_QUERY_EMPTY };
                }

                var permissionDtos = new List<PermissionDto>();

                foreach (var permissionItem in permissionItems)
                {
                    var permissionDto = new PermissionDto
                    {
                        Id = permissionItem.Id,
                        EmployeeForename = permissionItem.EmployeeForename,
                        EmployeeSurname = permissionItem.EmployeeSurname,
                        PermissionTypeId = permissionItem.PermissionTypeId,
                        PermissionType = await _dbContext.PermissionTypes
                                                                        .Where(x => x.Id == permissionItem.PermissionTypeId)
                                                                        .Select(x => x.PermissionDescription)
                                                                        .FirstOrDefaultAsync(),
                        PermissionGrantedOnDate = permissionItem.PermissionGrantedOnDate,
                        Message = ReplyMessages.MESSAGE_QUERY
                    };

                    permissionDtos.Add(permissionDto);
                }

                // Devuelvo la lista de objetos Permiso con todos los campos solicitados.
                return new PermissionDto
                {
                    Permissions = permissionDtos,
                };
            }
            catch (Exception)
            {
                // Envía un mensaje de error en caso de excepción.
                return new PermissionDto { Message = ReplyMessages.MESSAGE_FAILED };
            }
        }

        #endregion
    }
}
