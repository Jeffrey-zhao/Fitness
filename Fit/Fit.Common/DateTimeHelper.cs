using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class DateTimeHelper
  {
    public static DateTime GetNow()
    {
      return DateTime.Now;
    }

    public static DateTime GetToday()
    {
      return DateTime.Now.AddDays(0).Date;
    }

    public static long GetTicks(DateTime current)
    {
      var baseDate = new DateTime(1970, 1, 1);
      var span = current - baseDate;
      return (long)span.TotalMilliseconds;
    }

  }
}
