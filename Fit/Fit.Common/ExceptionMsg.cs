using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class ExceptionMsg
  {
    public static string GetEmailExistMsg(string phoneNum)
    {
      return string.Format("The email has exists: {0}", phoneNum);
    }

    public static string GetObjectNullMsg(string objName)
    {
      return string.Format("The object is null: {0}", objName);
    }
    
  }
}
