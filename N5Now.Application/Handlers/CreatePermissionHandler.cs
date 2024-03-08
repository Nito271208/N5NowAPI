using MediatR;
using Microsoft.EntityFrameworkCore;
using N5Now.Domain.DTOs;
using N5Now.Domain.Entities;
using N5Now.Infrastructure.Commands;
using N5Now.Infrastructure.Commons;
using N5Now.Infrastructure.Persistences.Contexts;
using N5Now.Utilities.Static;
using POS.Application.Validators.Category;
using System.ComponentModel;

namespace N5Now.Application.Handlers
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
    {
        #region Privates Variables

        private readonly DbApiContext _dbContext; 
        private readonly PermissionValidator _validationRules;
        private readonly CommonsPermission _commonsPermission;

        #endregion

        #region Constructor

        public CreatePermissionHandler(DbApiContext context,
            PermissionValidator validationRules,
            CommonsPermission commonsPermission)
        {
            _dbContext = context;
            _validationRules = validationRules;
            _commonsPermission = commonsPermission;
        }

        #endregion

        #region Principal Method Request Permission Handler
        public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validamos el request
                var validationResult = await _validationRules.ValidateAsync(request.CreatePermission!);

                // Si no hay Errores de Validacion, se puede agregar el Permiso
                if (validationResult.IsValid){

                    var permissionItem = new Permission
                    {
                        EmployeeForename = request.CreatePermission!.EmployeeForename,
                        EmployeeSurname = request.CreatePermission.EmployeeSurname,
                        PermissionTypeId = request.CreatePermission.PermissionTypeId,
                        PermissionGrantedOnDate = request.CreatePermission.PermissionGrantedOnDate,
                    };

                    _dbContext.Permissions.Add(permissionItem);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var requestElasticSearch = new PermissionDto
                    {
                        Id = permissionItem.Id,
                        EmployeeForename = permissionItem.EmployeeForename,
                        EmployeeSurname = permissionItem.EmployeeSurname,
                        PermissionTypeId = permissionItem.PermissionTypeId,
                        PermissionGrantedOnDate = permissionItem.PermissionGrantedOnDate,
                    };

                    await _commonsPermission.InsertIntoElasticSearch(requestElasticSearch);

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
                        Message = ReplyMessages.MESSAGE_SAVE
                    };
                }
                else { 
                    return new PermissionDto { Message = ReplyMessages.MESSAGE_VALIDATE};
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