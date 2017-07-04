using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.FrontWeb.Models
{
  public class CombineMotionAddModel
  {
    [Range(1, long.MaxValue)]
    public long PlanId { get; set; }
    [Range(1, long.MaxValue)]
    public long Index { get; set; }
    [Range(1, long.MaxValue)]
    public long PlanCount { get; set; }
    [Range(1, long.MaxValue)]
    public long MotionCombine { get; set; }
    [RegularExpression("^[0-9]+([.]{1}[0-9])?$")]
    public double Number { get; set; }
  }
}