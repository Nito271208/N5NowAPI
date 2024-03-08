using EvaluacionNetInfrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using N5Now.Domain.DTOs;
using N5Now.Utilities.Static;
using N5NowAPI.Controllers;

namespace N5NowUnitTes.Permissions
{

    [TestClass]
    public class PermissionsGetTest
    {
        [TestMethod]
        public async Task GetPermissions_ReturnsOk_WhenPermissionItemsExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetPermissionQuery>(), default))
                        .ReturnsAsync(new PermissionTypeDto { PermissionsListType = new List<PermissionTypeDto>() });

            var controller = new PermissionsTypeController(mediatorMock.Object);

            // Act
            var result = await controller.GetPermissions() as ActionResult<PermissionTypeDto>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task GetPermissions_ReturnsNotFound_WhenPermissionItemsDoNotExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetPermissionQuery>(), default))
                        .ReturnsAsync((PermissionTypeDto)null!);

            var controller = new PermissionsTypeController(mediatorMock.Object);

            // Act
            var result = await controller.GetPermissions();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            Assert.AreEqual(404, (result.Result as NotFoundObjectResult)?.StatusCode);
            Assert.AreEqual(ReplyMessages.MESSAGE_QUERY_EMPTY, (result.Result as NotFoundObjectResult)?.Value);
        }

    }
}
