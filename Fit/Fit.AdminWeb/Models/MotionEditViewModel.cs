using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class MotionEditViewModel
  {
    public MotionDTO Motion { get; set; }
    public string MotionType { get; set; }
    public List<MuscleGroupDTO> MuscleGroups { get; set; }
    public List<MuscleDTO> Muscles { get; set; }
  }
}