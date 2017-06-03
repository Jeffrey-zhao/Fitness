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
    public HomeController(IAdminUserService auService)
    {
      this.auService = auService;
    }
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }
  }
}