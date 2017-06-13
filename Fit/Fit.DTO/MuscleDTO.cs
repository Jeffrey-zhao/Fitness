using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class MuscleDTO
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long MuscleGroupID { get; set; }
    public string MuscleGroupName { get; set; }
  }
}
