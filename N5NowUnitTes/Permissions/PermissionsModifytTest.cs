using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using N5Now.Domain.DTOs;
using N5Now.Infrastructure.Commands;
using N5NowAPI.Controllers;

namespace N5NowUnitTes.Permissions
{

    [TestClass]
    public class PermissionsModifytTest
    {
        [TestMethod]
        public async Task Handle_ValidModification_ReturnsValidModifiedPermissionDto()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var command = new UpdatePermissionCommand
            {
                UpdatePermission = new PermissionDto
                {
                    Id = 1,
                    EmployeeForename = "Name",
                    EmployeeSurname = "lastName",
                    PermissionTypeId = 1,
                    PermissionGrantedOnDate = new DateOnly(2024, 03, 07)
                },
            };
            var modifiedPermissionDto = new PermissionDto
            {
                Id = 1,
                EmployeeForename = "Name",
                EmployeeSurname = "LastName",
                PermissionTypeId = 1,
                PermissionGrantedOnDate = new DateOnly(2024, 03, 07),
                PermissionType = "PermissionType",
                Message = "Se actualizó correctamente."
            };

            mediatorMock.Setup(x => x.Send(It.IsAny<UpdatePermissionCommand>(), default))
                        .ReturnsAsync(modifiedPermissionDto);

            var controller = new PermissionsController(mediatorMock.Object);

            // Act
            var result = await controller.ModifyPermission(command);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOfType(okResult.Value, typeof(PermissionDto));
            var returnedProduct = (PermissionDto)okResult.Value;
            Assert.AreEqual(modifiedPermissionDto.Id, returnedProduct.Id);
        }

        //    [TestMethod]
        //    public async Task Handle_InvalidModification_ReturnsPermissionDtoWithValidationMessage()
        //    {
        //        //Arrange
        //        var mediatorMock = new Mock<IMediator>();
        //        var command = new UpdatePermissionCommand
        //        {
        //            UpdatePermission = new PermissionDto
        //            {
        //                Id = 1,
        //                EmployeeForename = "",
        //                EmployeeSurname = "Lastname",
        //                PermissionTypeId = 1,
        //                PermissionGrantedOnDate = new DateOnly(2024,03,07)
        //            },
        //        };
        //        //var permissionDto = new PermissionDto
        //        //{
        //        //    Id = 1,
        //        //    EmployeeForename = "",
        //        //    EmployeeSurname = "",
        //        //    PermissionTypeId = 1,
        //        //    PermissionGrantedOnDate = new DateOnly(2024, 03, 07)
        //        //};

        //        // Simulate an invalid modification by returning null
        //        mediatorMock.Setup(x => x.Send(It.IsAny<UpdatePermissionCommand>(), default))
        //                    .ReturnsAsync((PermissionDto)null!);

        //        var controller = new PermissionsController(mediatorMock.Object);

        //        //Act
        //        var result = await controller.ModifyPermission(command);

        //        //Assert
        //        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        //        var badRequestResult = (BadRequestObjectResult)result.Result;
        //        Assert.AreEqual(ReplyMessages.MESSAGE_VALIDATE, badRequestResult.Value!.ToString());
        //    }

    }
}
