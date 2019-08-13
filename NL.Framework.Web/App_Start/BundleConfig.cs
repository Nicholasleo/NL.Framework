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
                                   "~/Scripts/layui/src/nlframe/layui/layui.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Scripts/layui/src/nlframe/layui/css/layui.css",
                      "~/Scripts/layui/src/nlframe/style/admin.css",
                      "~/Scripts/layui/src/nlframe/layui/css/modules/layui-icon-extend/iconfont.css"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                      "~/Scripts/layui/src/nlframe/style/login.css"));

            bundles.Add(new StyleBundle("~/bundles/dtree").Include(
                      "~/Scripts/layui/src/nlframe/style/dtree.css",
                      "~/Scripts/layui/src/nlframe/layui/css/modules/dtree/dtreefont.css"));


        }
    }
}
