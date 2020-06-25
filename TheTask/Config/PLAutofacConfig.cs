using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BL.Config;
using BL.Interfaces;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheTask.Config
{
    public class PLAutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var config = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>() { new PLMapper(), new BLMapper() }));
            builder.Register(c => config.CreateMapper());

            builder.RegisterType<DeptService>().As<IDeptService>();
            builder.RegisterType<EmpService>().As<IEmpService>();
            builder.RegisterModule<BLAutofacConfig>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}