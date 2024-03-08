using N5Now.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5Now.Domain.Entities;

public partial class PermissionType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string PermissionDescription { get; set; } = null!;
    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
