using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using N5Now.Domain.DTOs;
using N5Now.Infrastructure.Commands;
using N5Now.Utilities.Static;
using N5NowAPI.Controllers;

namespace N5NowUnitTes.Permissions
{

    [TestClass]
    public class PermissionsRequestTest
    {
        [TestMethod]
        public async Task Handle_ValidRequest_ReturnsValidPermissionDto()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var command = new CreatePermissionCommand
            {
                CreatePermission = new PermissionDto
                {
                    EmployeeForename = "Name",
                    EmployeeSurname = "lastName",
                    PermissionTypeId = 1,
                    PermissionGrantedOnDate = new DateOnly(2024, 03, 07)
                },
            };
            var permissionDto = new PermissionDto
            {
                Id = 0,
                EmployeeForename = "Name",
                EmployeeSurname = "lastName",
                PermissionTypeId = 1,
                PermissionGrantedOnDate = new DateOnly(2024, 03, 07)
            };

            mediatorMock.Setup(x => x.Send(It.IsAny<CreatePermissionCommand>(), default))
                        .ReturnsAsync(permissionDto);

            var controller = new PermissionsController(mediatorMock.Object);

            // Act
            var result = await controller.RequestPermission(command);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOfType(okResult.Value, typeof(PermissionDto));
            var returnedProduct = (PermissionDto)okResult.Value;
            Assert.AreEqual(permissionDto.Id, returnedProduct.Id);
        }

        [TestMethod]
        public async Task Handle_InvalidRequest_ReturnsPermissionDtoWithValidationMessage()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var command = new CreatePermissionCommand
            {
                CreatePermission = new PermissionDto
                {
                    EmployeeForename = "",
                    EmployeeSurname = "",
                    PermissionTypeId = 0,
                    PermissionGrantedOnDate = new DateOnly()
                },
            };

            // Simulo un request invalido para que me devuelva un Task<PermissionDto> en null
            mediatorMock.Setup(x => x.Send(It.IsAny<CreatePermissionCommand>(), default))
                        .ReturnsAsync((PermissionDto)null!);

            var controller = new PermissionsController(mediatorMock.Object);

            // Act
            var result = await controller.RequestPermission(command);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.AreEqual(ReplyMessages.MESSAGE_VALIDATE, badRequestResult.Value!.ToString());
        }
    }
}
