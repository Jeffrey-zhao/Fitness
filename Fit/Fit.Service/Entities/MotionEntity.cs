using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class MotionEntity : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public long? MuscleID { get; set; }
    public string Detail { get; set; }
    public string MainPoint { get; set; }
    public string Attention { get; set; }
    public string Measurement { get; set; }
    public virtual MuscleEntity Muscle { get; set; }
    public virtual ICollection<MotionPicEntity> MotionPics { get; set; }
    public virtual ICollection<MotionsInPlanEntity> MotionsInPlan { get; set; }
  }
}
