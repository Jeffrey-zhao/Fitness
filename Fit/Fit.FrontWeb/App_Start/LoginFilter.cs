using Fit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.App_Start
{
  public class LoginFilter : IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationContext filterContext)
    {
      LoginAttribute[] attrs = filterContext.ActionDescriptor
        .GetCustomAttributes(typeof(LoginAttribute), false) as LoginAttribute[];
      if (attrs.Length <= 0) return;

      var loginID = MVCHelper.GetLoginIdFromSession(filterContext.HttpContext);
      if (!loginID.HasValue)
      {
        if (filterContext.HttpContext.Request.IsAjaxRequest())
        {
          var ajaxResult = new AjaxResult
          {
            Status = AjaxResultEnum.redirect.ToString(),
            Data = "/User/Login",
            Msg = "Should Login"
          };
          filterContext.Result = MVCHelper.GetJsonResult(ajaxResult);
        }
        filterContext.Result = new RedirectResult("/User/Login");
        return;
      }
    }
  }
}