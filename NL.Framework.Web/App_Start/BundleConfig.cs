using System.Web;
using System.Web.Optimization;

namespace NL.Framework.Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/layui").Include(
                                   "~/Scripts/layui/src/layuiadmin/layui/layui.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Scripts/layui/src/layuiadmin/layui/css/layui.css",
                      "~/Scripts/layui/src/layuiadmin/style/admin.css",
                      "~/Scripts/layui/src/layuiadmin/layui/css/modules/layui-icon-extend/iconfont.css"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                      "~/Scripts/layui/src/layuiadmin/style/login.css"));

            bundles.Add(new StyleBundle("~/bundles/eleTree").Include(
                      "~/Scripts/layui/src/layuiadmin/style/eleTree.css"));


        }
    }
}
