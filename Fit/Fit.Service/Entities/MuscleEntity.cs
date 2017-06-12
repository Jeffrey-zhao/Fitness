using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class MuscleEntity : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public long MuscleGroupID { get; set; }
    public virtual MuscleGroupEntity MuscleGroup { get; set; }
    public virtual ICollection<MotionEntity> Motions { get; set; }
  }
}
