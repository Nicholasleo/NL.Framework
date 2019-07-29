using Autofac;
using System;
using Autofac.Integration.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NL.Framework.DAL;
using NL.Framework.IDAL;
using NL.Framework.BLL;
using NL.Framework.IBLL;
//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-27 16:33:03 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Web.App_Start
{
    public partial class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<NLFrameContext>().As<IDbContext>();
            builder.RegisterType<SystemInit>().As<ISystemInit>();
            builder.RegisterType<MenuBll>().As<IMenuBll>();
            builder.RegisterType<RoleBll>().As<IRoleBll>();
            //autofac 注册依赖
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}
