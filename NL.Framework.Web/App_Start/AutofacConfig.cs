using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NL.Framework.Common.Log;
//using NL.Framework.IBLL;
//using NL.Framework.IDAL;
using System.Reflection;
using System.Web.Mvc;
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
            var config = new ConfigurationBuilder();
            IConfigurationSource autofacJsonConfigSource = new JsonConfigurationSource()
            {
                Path = "/Configs/Autofac.json",
                Optional = false,//boolean,默认就是false,可不写
                ReloadOnChange = false,//同上
            };
            config.Add(autofacJsonConfigSource);
            var module = new ConfigurationModule(config.Build());

            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterType<NLFrameContext>().As<IDbContext>();
            //builder.RegisterType<SystemInit>().As<ISystemInit>();
            //builder.RegisterType<MenuBll>().As<IMenuBll>();
            //builder.RegisterType<RoleBll>().As<IRoleBll>();
            //autofac 注册依赖
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }

        public static void Register()
        {
            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<RoleAuthorizeServices>().As<IRoleAuthorizeServices>();
            //builder.RegisterType<HandlerAuthorizeAttribute>().SingleInstance();//注意，这里要把我们的全局Filter注册到Autofac中
            //builder.RegisterType<LogServices>().As<ILogServices>();
            //builder.RegisterType<HandlerErrorAttribute>().SingleInstance();
            //builder.RegisterFilterProvider();

            //告诉autofac框架注业务逻辑层接口所在程序集中的所有类的对象实例
            Assembly ibll = Assembly.Load("NL.Framework.IBLL");
            //创建ibll中的所有类的实例以此类的实现接口存储
            builder.RegisterTypes(ibll.GetTypes()).AsImplementedInterfaces();

            //告诉autofac框架注册数据访问层接口所在程序集中的所有类的对象实例
            Assembly idal = Assembly.Load("NL.Framework.IDAL");
            //创建idal中的所有类的实例以此类的实现接口存储
            builder.RegisterTypes(idal.GetTypes()).AsImplementedInterfaces();

            //告诉autofac框架注册业务处理层所在程序集中的所有类的对象实例
            Assembly bll = Assembly.Load("NL.Framework.BLL");
            //创建respAss中的所有类的实例以此类的实现接口存储
            builder.RegisterTypes(bll.GetTypes()).AsImplementedInterfaces();

            //告诉autofac框架注册数据访问层所在程序集中的所有类的对象实例
            Assembly dal = Assembly.Load("NL.Framework.DAL");
            //创建dal中的所有类的实例以此类的实现接口存储
            builder.RegisterTypes(dal.GetTypes()).AsImplementedInterfaces();


            builder.RegisterType<Logger>().As<ILogger>();

            //创建一个Autofac的容器
            var container = builder.Build();
            //将MVC的控制器对象实例 交由autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
