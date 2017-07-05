using Autofac;
using Autofac.Integration.Mvc;
using Fit.FrontWeb.Controllers;
using Fit.IService;
using Fit.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.App_Start
{
  public class AutofacConfig
  {
    public static void Config()
    {
      var builder = new ContainerBuilder();
      builder.RegisterControllers(typeof(MvcApplication).Assembly);
      Assembly[] assemblies = new Assembly[] { Assembly.Load("Fit.Service") };
      builder.RegisterAssemblyTypes(assemblies)
        .Where(a => !a.IsAbstract && typeof(IServiceSupport).IsAssignableFrom(a)
        ).AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(assemblies)
        .Where(a => !a.IsAbstract && a.Name.EndsWith("Repository")).AsImplementedInterfaces();
      //builder.Register(typeof(UserService)).As(typeof(IUserService));

      //builder.Register(a => new PlanController((IPlanService)a.Resolve(typeof(IPlanService)), (ISecheduleService)a.Resolve(typeof(ISecheduleService)), (IKeyValueService)a.Resolve(typeof(IKeyValueService))));

      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
  }
}