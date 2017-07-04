using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class SecheduleEntity : BaseEntity
  {
    public DateTime ActDate { get; set; }
    public long PlanID { get; set; }
    public bool IsFinished { get; set; } = false;
  }
}
