using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class MotionsInPlanInputDTO
  {
    public long PlanID { get; set; }
    public long MotionID { get; set; }
    public int Groups { get; set; }
    public int Times { get; set; }
    public double Number { get; set; }
  }
}
