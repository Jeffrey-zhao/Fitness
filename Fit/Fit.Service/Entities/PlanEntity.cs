using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class PlanEntity : BaseEntity
  {
    public long UserID { get; set; }
    public virtual UserEntity User { get; set; }
    public virtual ICollection<MotionsInPlanEntity> MotionsInPlans { get; set; }
  }
}
