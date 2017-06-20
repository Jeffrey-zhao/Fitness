using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class Enums
  {
    public enum PicType
    {
      Detail=1,
      Attention=2,
      MainPoint=3
    }

    public enum MotionType
    {
      /// <summary>
      /// 局部训练
      /// </summary>
      Partial,
      /// <summary>
      /// 综合训练
      /// </summary>
      Combine
    }
  }
}
