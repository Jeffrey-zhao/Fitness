using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class SecheduleDetailEntity : BaseEntity
  {
    public long SecheduleID { get; set; }
    public long MotionsInPlanID { get; set; }
    public bool IsFinished { get; set; } = false;
  }
}
