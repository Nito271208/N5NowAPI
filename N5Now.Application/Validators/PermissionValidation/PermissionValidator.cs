using FluentValidation;
using N5Now.Domain.DTOs;

namespace POS.Application.Validators.Category
{
    public class PermissionValidator : AbstractValidator<PermissionDto>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.EmployeeForename)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio.");
            RuleFor(x => x.EmployeeSurname)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio.");
            RuleFor(x => x.PermissionTypeId)
                .NotNull().WithMessage("El Id para el tipo de permiso no puede ser nulo.")
                .NotEmpty().WithMessage("El Id para el de permiso no puede estar vacio.");
            RuleFor(x => x.PermissionGrantedOnDate)
                .NotNull().WithMessage("Debe especificar una fecha.")
                .NotEmpty().WithMessage("El tipo de permiso no puede estar vacio.");
        }
    }
}
