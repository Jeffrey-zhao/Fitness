using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class BodyCircumferenceEntity : BaseEntity
  {
    public long UserID { get; set; }
    public double UpperArm { get; set; } = 0;
    public double LowerArm { get; set; } = 0;
    public double Chest { get; set; } = 0;
    public double Waist { get; set; } = 0;
    public double Hip { get; set; } = 0;
    public double UpperLeg { get; set; } = 0;
    public double LowerLeg { get; set; } = 0;
    public double Weight { get; set; } = 0;
  }
}
