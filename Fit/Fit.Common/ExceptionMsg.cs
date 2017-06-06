using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class ExceptionMsg
  {
    public static string GetObjExistMsg(string objName,string objValue)
    {
      return string.Format("The {0} has exists: {1}", objName,objValue);
    }
    public static string GetObjectNullMsg(string objName)
    {
      return string.Format("The object is null: {0}", objName);
    }
    
  }
}
