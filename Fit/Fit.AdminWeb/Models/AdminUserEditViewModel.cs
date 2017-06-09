using Fit.DTO.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class AdminUserEditViewModel
  {
    public long ID { get; set; }
    public string Name { get; set; }
    public string PhoneNum { get; set; }
    public string Email { get; set; }
    public long[] RoleIDs { get; set; }
    public RoleDTO[] AllRoles { get; set; }
  }
}