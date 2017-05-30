using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class ExceptionMsg
  {
    public static string GetPhoneNumExistMsg(string phoneNum)
    {
      return string.Format("The phonenum has exists: {0}", phoneNum);
    }

    public static string GetObjectNullMsg(object obj)
    {
      return string.Format("The object is null: ", nameof(obj));
    }
  }
}
