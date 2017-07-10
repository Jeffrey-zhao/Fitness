using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class BodyCircumferenceDTO
  {
    public long ID { get; set; }
    public long UserID { get; set; }
    public double UpperArm { get; set; }
    public double LowerArm { get; set; }
    public double Chest { get; set; }
    public double Waist { get; set; }
    public double Hip { get; set; }
    public double UpperLeg { get; set; }
    public double LowerLeg { get; set; }
    public double Weight { get; set; }
  }
}
