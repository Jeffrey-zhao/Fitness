using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class MotionAddModel
  {
    public long MuscleID { get; set; }
    [Required]
    public string MotionType { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(512)]
    public string Description { get; set; }
    [MaxLength(1024)]
    public string Attention { get; set; }
    [MaxLength(1024)]
    public string Detail { get; set; }
    [MaxLength(1024)]
    public string MainPoint { get; set; }
  }
}