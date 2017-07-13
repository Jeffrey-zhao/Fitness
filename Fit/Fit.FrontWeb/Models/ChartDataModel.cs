using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fit.FrontWeb.Models
{
  public class ChartDataModel
  {
    public LineChartDTO[] Data { get; set; }
    public string Legend { get; set; }
  }
}