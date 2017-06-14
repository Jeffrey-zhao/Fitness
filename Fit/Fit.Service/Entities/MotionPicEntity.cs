using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class MotionPicEntity:BaseEntity
  {
    public int PicType { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public long MotionID { get; set; }
    public virtual MotionEntity Motion { get; set; }
  }
}
