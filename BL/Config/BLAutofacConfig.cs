using Autofac;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Config
{
    public class BLAutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(DeptRepository)).As(typeof(IDeptRepository));
            builder.RegisterType(typeof(EmpRepository)).As(typeof(IEmpRepository));
        }
    }
}
