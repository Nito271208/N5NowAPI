using MediatR;
using Microsoft.EntityFrameworkCore;
using N5Now.Domain.DTOs;
using N5Now.Infrastructure.Commands;
using N5Now.Infrastructure.Commons;
using N5Now.Infrastructure.Persistences.Contexts;
using N5Now.Utilities.Static;
using POS.Application.Validators.Category;

namespace N5Now.Application.Handlers
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
    {
        #region Privates Variables

        private readonly DbApiContext _dbContext;
        private readonly PermissionValidator _validationRules;
        private readonly CommonsPermission _commonsPermission;

        #endregion

        #region Constructor

        public UpdatePermissionHandler(DbApiContext context,
            PermissionValidator validationRules,
            CommonsPermission commonsPermission)
        {
            _dbContext = context;
            _validationRules = validationRules;
            _commonsPermission = commonsPermission;
        }

        #endregion

        #region Principal Method Modify Permission Handler
        public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var permissionItem = await _dbContext.Permissions.FindAsync(
                    new object[] { request.UpdatePermission!.Id }, cancellationToken);

                if (permissionItem == null)
                {
                    return new PermissionDto { Message = ReplyMessages.MESSAGE_QUERY_EMPTY };
                }

                // Validamos el request
                var validationResult = await _validationRules.ValidateAsync(request.UpdatePermission!);

                // Si no hay Errores, se puede actualizar el Permiso
                if (validationResult.IsValid)
                {
                    permissionItem.EmployeeForename = request.UpdatePermission!.EmployeeForename;
                    permissionItem.EmployeeSurname = request.UpdatePermission.EmployeeSurname;
                    permissionItem.PermissionTypeId = request.UpdatePermission.PermissionTypeId;
                    permissionItem.PermissionGrantedOnDate = request.UpdatePermission.PermissionGrantedOnDate;

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var modifyElasticSearch = new PermissionDto
                    {
                        Id = permissionItem.Id,
                        EmployeeForename = permissionItem.EmployeeForename,
                        EmployeeSurname = permissionItem.EmployeeSurname,
                        PermissionTypeId = permissionItem.PermissionTypeId,
                        PermissionGrantedOnDate = permissionItem.PermissionGrantedOnDate,
                    };

                    await _commonsPermission.InsertIntoElasticSearch(modifyElasticSearch);

                    return new PermissionDto
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
                        Message = ReplyMessages.MESSAGE_UPDATE,
                    };
                }
                else
                {
                    return new PermissionDto { Message = ReplyMessages.MESSAGE_VALIDATE };
                }
            }
            catch (Exception)
            {
                // Envía un mensaje de error en caso de excepción
                return new PermissionDto { Message = ReplyMessages.MESSAGE_FAILED };
            }
        }
        #endregion    
    }
}
