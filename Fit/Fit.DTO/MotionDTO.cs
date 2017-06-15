using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class MotionDTO
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Detail { get; set; }
    public Dictionary<long, string> DetailDic { get; set; }
    public string MainPoint { get; set; }
    public Dictionary<long,string> MainPointDic { get; set; }
    public string Attention { get; set; }
    public Dictionary<long, string> AttentionDic { get; set; }
    public long MuscleID { get; set; }
    public string MuscleName { get; set; }
  }
}
