using Fit.AdminWeb.App_Start;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class HomeController : Controller
  {
    private IAdminUserService auService;
    //private IUserService userService;
    public HomeController(IAdminUserService auService/*, IUserService userService*/)
    {
      this.auService = auService;
    }

    //[Permission("Home.Index")]
    public ActionResult Index()
    {
      return View();
    }
  }
}