using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fit.Common
{
  public enum AjaxResultEnum
  {
    ok,
    error,
    fail,
    redirect
  }

  public class MVCHelper
  {
    public static string GetValidMsg(ModelStateDictionary modelState)
    {
      StringBuilder sb = new StringBuilder();
      foreach (var key in modelState.Keys)
      {
        if (modelState[key].Errors.Count <= 0)
        {
          continue;
        }
        sb.Append("属性【").Append(key).Append("】错误：");
        foreach (var modelError in modelState[key].Errors)
        {
          sb.AppendLine(modelError.ErrorMessage);
        }
      }
      return sb.ToString();
    }

    public static JsonResult GetJsonResult(AjaxResultEnum status, string msg = "")
    {
      return new JsonResult
      {
        Data = new AjaxResult
        {
          Status = status.ToString(),
          Msg = msg
        }
      };
    }

    public static JsonResult GetJsonResult(AjaxResult ajaxResult)
    {
      return new JsonResult { Data = ajaxResult };
    }

    public static void SetLoginInfoToSession(HttpContextBase ctx, long id, string email)
    {
      ctx.Session[Consts.LOGIN_ID] = id;
      ctx.Session[Consts.LOGIN_EMAIL] = email;
    }
    public static long? GetLoginIdFromSession(HttpContextBase ctx)
    {
      return (long?)ctx.Session[Consts.LOGIN_ID];
    }
    public static string GetLoginEmailFromSession(HttpContextBase ctx)
    {
      var email = string.Empty;
      if (ctx.Session[Consts.LOGIN_EMAIL] != null)
      {
        email = ctx.Session[Consts.LOGIN_EMAIL].ToString();
      }
      return email;
    }

    public static string RenderViewToString(ControllerContext context, string viewPath, object model = null)
    {
      ViewEngineResult viewEnginResult = ViewEngines.Engines.FindView(context, viewPath, null);
      if (viewEnginResult == null)
      {
        throw new FileNotFoundException("View " + viewPath + "cannot be found");
      }
      var view = viewEnginResult.View;
      context.Controller.ViewData.Model = model;
      using (var sw = new StringWriter())
      {
        var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
        view.Render(ctx, sw);
        return sw.ToString();
      }
    }
  }
}
