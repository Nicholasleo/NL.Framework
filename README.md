# NL.Framework
ASP.NET MVC 5 ,Entity Framework 6,Autofac


系统首次运行时，可以通过数据初始化操作进行基础测试
初始化方式是：在LoginController中将对应的初始化功能注释去掉既可以

public ActionResult Index()
{

   //初始化菜单
   
   //_ISystemInit.InitMenu();
   
   //初始化功能
   
   //_ISystemInit.InitFunction();
   
   //初始化菜单功能关系
   
   //_ISystemInit.InitMenuFunction();
   
   //初始化角色
   
   _ISystemInit.InitRole();
   
   //初始化用户
   
   _ISystemInit.InitUser();
   
   //初始化用户角色
   
   _ISystemInit.InitUserRole();
   
   return View();        
}
