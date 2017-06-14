using Fit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class UploadTestController : Controller
  {
    [HttpGet]
    public ActionResult Upload()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Upload(long id)
    {
      //HttpPostedFileBase file = Request.Files["file1"];
      //file.SaveAs(Server.MapPath("~/a.png"));
      //var length = file.ContentLength;
      SaveImgInCloud.Save();
      return View();
    }
  }
}