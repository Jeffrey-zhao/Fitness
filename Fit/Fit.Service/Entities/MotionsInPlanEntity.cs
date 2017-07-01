using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class MotionsInPlanEntity:BaseEntity
  {
    public long ID { get; set; }
    public long PlanID { get; set; }
    public long MotionID { get; set; }
    public int Groups { get; set; }
    public int Times { get; set; }
    public double Number { get; set; }
    public virtual PlanEntity Plan { get; set; }
    public virtual MotionEntity Motion { get; set; }
  }
}
