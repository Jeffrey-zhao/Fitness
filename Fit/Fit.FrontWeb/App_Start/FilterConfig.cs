using Fit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.App_Start
{
  public class FilterConfig
  {
    public static void RegisterConfig(GlobalFilterCollection collection)
    {
      collection.Add(new JsonNetActionFilter());
      collection.Add(new ExceptionFilter());
      collection.Add(new LoginFilter());
    }
  }
}