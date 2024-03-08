using N5Now.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5Now.Domain.DTOs
{
    public class PermissionTypeDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PermissionDescription { get; set; } = null!;
        public List<PermissionTypeDto>? PermissionsListType { get; set; }
    }
}
