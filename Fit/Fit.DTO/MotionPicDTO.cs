using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fit.Common.Enums;

namespace Fit.DTO
{
  public class MotionPicDTO
  {
    public long ID { get; set; }
    public int PicType { get; set; }
    public string Url { get; set; }
    public long MotionID { get; set; }
  }
}
