using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class MotionAddModel
  {
    public long MuscleID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Attention { get; set; }
    public string Detail { get; set; }
    public string MainPoint { get; set; }
  }
}