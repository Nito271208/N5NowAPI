using EvaluacionNetInfrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5Now.Domain.DTOs;
using N5Now.Utilities.Static;

namespace N5NowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsTypeController : ControllerBase
    {
        #region Privates variables

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public PermissionsTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Principal Methods Permissions Type Controller

        [HttpGet]
        public async Task<ActionResult<PermissionTypeDto>> GetPermissions()
        {
            var query = new GetPermissionQuery();
            var permissionItem = await _mediator.Send(query);
            if (permissionItem == null)
            {
                return NotFound(ReplyMessages.MESSAGE_QUERY_EMPTY);
            }
            return Ok(permissionItem);
        }

        #endregion
    }
}
