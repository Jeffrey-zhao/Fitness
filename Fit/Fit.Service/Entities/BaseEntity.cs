using Fit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class BaseEntity
  {
    public long ID { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedDateTime { get; set; } = DateTimeHelper.GetNow();
  }
}
