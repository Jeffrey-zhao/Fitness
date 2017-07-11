using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.FrontWeb.Models
{
  public class BodyCircumferenceModel
  {
    public long ID { get; set; }
    [Range(1, long.MaxValue)]
    public long UserID { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? UpperArm { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? LowerArm { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? Chest { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? Waist { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? Hip { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? UpperLeg { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? LowerLeg { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double? Weight { get; set; }
  }
}