using System.ComponentModel.DataAnnotations.Schema;

namespace N5Now.Domain.Entities;

public partial class Permission
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string EmployeeForename { get; set; } = null!;
    public string EmployeeSurname { get; set; } = null!;
    public int PermissionTypeId { get; set; }
    public DateOnly PermissionGrantedOnDate { get; set; }
    public virtual PermissionType PermissionType { get; set; } = null!;
}
