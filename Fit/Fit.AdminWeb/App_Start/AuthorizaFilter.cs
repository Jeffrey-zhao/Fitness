using Fit.Common;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.App_Start
{
  public class AuthorizaFilter : IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationContext filterContext)
    {
      PermissionAttribute[] attrs = filterContext.ActionDescriptor
        .GetCustomAttributes(typeof(PermissionAttribute), false)
        as PermissionAttribute[];
      if (attrs.Length <= 0) return;

      var userId = MVCHelper.GetLoginIdFromSession(filterContext.HttpContext);
      if (userId == null)
      {
        if (filterContext.HttpContext.Request.IsAjaxRequest())
        {
          var ajaxResult = new AjaxResult
          {
            Status = AjaxResultEnum.redirect.ToString(),
            Data = "/AdminUser/Login",
            Msg = "Should Login"
          };
          filterContext.Result = MVCHelper.GetJsonResult(ajaxResult);
        }
        filterContext.Result = new RedirectResult("/AdminUser/Login");
        return;
      }
      var adminUserService = DependencyResolver.Current.GetService<IAdminUserService>();
      foreach (var item in attrs)
      {
        if (adminUserService.CheckPermission(userId.Value, item.Permission))
        {
          filterContext.Result = new ContentResult { Content = "You don't have the permission of " + item.Permission };
        }
      }
    }
  }
}