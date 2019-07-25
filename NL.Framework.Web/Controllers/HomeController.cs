using NL.Framework.DAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new NLFrameContext())
            {
                UserModel user = new UserModel {
                    UserName = "NicholasLeo",
                    UserCode = "1001",
                    UserPwd = "123456",
                    CreatePerson = "NicholasLeo",
                    CreateTime = DateTime.Now.Date
                };
                db.Users.Add(user);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    Console.WriteLine(i);
                }
            }
                ViewBag.Title = "测试";
            ViewBag.SystemName = "NLFrame";
            ViewBag.UserName = "NicholasLeo";
             HomeModel model = new HomeModel();
            List<TopMenu> topList = new List<TopMenu> {
                new TopMenu{
                    Name="Top1"
                    ,Url="home"
                    ,Code="Top1"
                    ,Childs=new List<Menu>{
                        new Menu{
                            Name = "TC1",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC2",
                            Code = "TC2",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC3",
                            Code = "TC3",
                            Url = "ddd"
                        }
                    }
                },
                 new TopMenu{
                    Name="Top2"
                    ,Url="home"
                    ,Childs=new List<Menu>{
                        new Menu{
                            Name = "TC1",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC2",
                            Code = "TC2",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC3",
                            Code = "TC3",
                            Url = "ddd"
                        }
                    }
                }
            };
            List<NvaMenu> nvaList = new List<NvaMenu> {
                new NvaMenu{
                    Name="NVA1"
                    ,Url="home",
                    HasChild = true
                    ,Childs=new List<Menu>{
                        new Menu{
                            Name = "TC1",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC2",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC3",
                            Code = "TC1",
                            Url = "ddd"
                        }
                    }
                },
                 new NvaMenu{
                    Name="NVA2"
                    ,Url="home",
                    HasChild = true
                    ,Childs=new List<Menu>{
                        new Menu{
                            Name = "TC1",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC2",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC3",
                            Code = "TC1",
                            Url = "ddd"
                        }
                    }
                },
                 new NvaMenu{
                    Name="NVA3"
                    ,Url="home",
                    HasChild = false
                    ,Childs=new List<Menu>{
                        new Menu{
                            Name = "TC1",
                            Code = "TC1",
                            Url = "ddd"
                        },
                        new Menu{
                            Code = "TC1",
                            Name = "TC2",
                            Url = "ddd"
                        },
                        new Menu{
                            Name = "TC3",
                            Code = "TC1",
                            Url = "ddd"
                        }
                    }
                }
            };
            model.NvaMenuLists = nvaList;
            model.TopMenuLists = topList;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}