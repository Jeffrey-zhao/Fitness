using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class RoleModel
  {
    public long ID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(512)]
    public string Description { get; set; }
  }
}