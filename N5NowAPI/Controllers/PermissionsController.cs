using EvaluacionNetInfrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Now.Domain.DTOs;
using N5Now.Domain.Entities;
using N5Now.Infrastructure.Commands;
using N5Now.Utilities.Static;

namespace N5NowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        #region Privates Variables

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Principal Methods Permissions Controller

        [HttpGet]
        public async Task<ActionResult<PermissionDto>> GetAllPermissions()
        {
            var query = new GetAllPermissionQuery();
            var permissionDto = await _mediator.Send(query);
            if (permissionDto == null || permissionDto.Permissions == null || !permissionDto.Permissions.Any())
            {
                return NotFound(permissionDto?.Message ?? ReplyMessages.MESSAGE_QUERY_EMPTY);
            }
            return Ok(permissionDto.Permissions);
        }

        [HttpPost]
        public async Task<ActionResult<Permission>> RequestPermission(CreatePermissionCommand command)
        {
            try
            {
                var permissionItem = await _mediator.Send(command);
                if (permissionItem == null)
                {
                    return BadRequest(ReplyMessages.MESSAGE_VALIDATE);
                }
                else
                {
                    return Ok(permissionItem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $" {ReplyMessages.MESSAGE_FAILED} {ex.Message}" });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Permission>> ModifyPermission(UpdatePermissionCommand command)
        {
            try
            {
                var permissionItem = await _mediator.Send(command);

                if (permissionItem.Message == ReplyMessages.MESSAGE_QUERY_EMPTY)
                {
                    return NotFound(ReplyMessages.MESSAGE_QUERY_EMPTY);
                }
                else if (permissionItem.Message == ReplyMessages.MESSAGE_UPDATE)
                {
                    return Ok(permissionItem);
                }
                else 
                {
                    return BadRequest(ReplyMessages.MESSAGE_VALIDATE);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $" {ReplyMessages.MESSAGE_FAILED} {ex.Message}" });
            }
        }

        #endregion
    }
}
