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
    public virtual MuscleEntity Muscle { get; set; }
  }
}
