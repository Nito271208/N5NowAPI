using EvaluacionNetInfrastructure.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N5Now.Domain.DTOs;
using N5Now.Domain.Entities;
using N5Now.Infrastructure.Commons;
using N5Now.Infrastructure.Persistences.Contexts;
using N5Now.Utilities.Static;
using Nest;

namespace N5Now.Application.Handlers
{
    public class GetPermissionHandler : IRequestHandler<GetPermissionQuery, PermissionTypeDto>
    {
        #region Privates Variables

        private readonly DbApiContext _dbContext;
        private readonly CommonsPermission _commonsPermission;

        #endregion

        #region Constructor

        public GetPermissionHandler(DbApiContext context,
            CommonsPermission commonsPermission)
        {
            _dbContext = context;
            _commonsPermission = commonsPermission;
        }

        #endregion

        #region Principal Method Permission List Handler
        public async Task<PermissionTypeDto> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
          try
            {
                //Realizamos la busqueda en la Tabla PermissionTypes.
                var permissionItem = await _dbContext.PermissionTypes
                                                                  .AsNoTracking()
                                                                  .Where(p => p.Id != 0)
                                                                  .ToListAsync();

                if (permissionItem == null || !permissionItem.Any())
                {
                    return new PermissionTypeDto { PermissionsListType = null! };
                }


                var permissionTypeDtos = new List<PermissionTypeDto>();

                foreach (var permissionItems in permissionItem)
                {
                    var elasticSearch = new PermissionTypeDto
                    {
                        Id = permissionItems.Id,
                        PermissionDescription = permissionItems.PermissionDescription,
                    };
                    permissionTypeDtos.Add(elasticSearch);
                }

                await _commonsPermission.InsertPermissionTypeIntoElasticSearch(permissionTypeDtos);

                // Devuelvo mi Lista de la Tabla PermissionTypes.
                return new PermissionTypeDto
                {
                    PermissionsListType = permissionTypeDtos,
                };
            }
            catch (Exception)
            {
                // Envía un mensaje de error en caso de excepción.
                return new PermissionTypeDto { PermissionsListType = null! };
            }
        }
        #endregion
    }
}
