using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class AdminLogDTO
  {
    public string Name { get; set; }
    public long AdminID { get; set; }
    public string AdminName { get; set; }
    public DateTime CreateDateTime { get; set; }
  }
}
