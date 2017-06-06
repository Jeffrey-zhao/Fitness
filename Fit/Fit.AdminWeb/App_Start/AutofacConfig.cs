using Autofac;
using Autofac.Integration.Mvc;
using Fit.IService;
using Fit.Service.Entities.RBAC;
using Fit.Service.Repository;
using Fit.Service.Services.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.App_Start
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
      //builder.Register(typeof(AdminUserRepository)).As(typeof(IRepository<AdminUserEntity>));

      builder.Register(a => new AdminUserService(
          (IRepository<AdminUserEntity>)a.Resolve(typeof(IRepository<AdminUserEntity>))
          ));

      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
  }
}