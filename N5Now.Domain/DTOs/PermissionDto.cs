using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5Now.Domain.DTOs
{
    public class PermissionDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public int PermissionTypeId { get; set; }
        public DateOnly PermissionGrantedOnDate { get; set; }
        public string? PermissionType { get; set; }
        public string? Message { get; set; }
        public List<PermissionDto>? Permissions { get; set; }
    }
}
