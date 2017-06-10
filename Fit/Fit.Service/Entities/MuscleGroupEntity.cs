using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class MuscleGroupEntity : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<MuscleEntity> Muscles { get; set; }
  }
}
