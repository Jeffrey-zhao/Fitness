using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class CurrentItemDTO
  {
    public long SecheduleDetailID { get; set; }
    public string ItemName { get; set; }
    public string ItemBurden { get; set; }
    public bool IsFinished { get; set; }
  }
}
