using System.Web;
using System.Web.Optimization;

namespace NL.Framework.Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

#if DEBUG
            bundles.Add(new ScriptBundle("~/bundles/layui").Include(
                        "~/Scripts/layui/src/layuiadmin/layui/layui.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Scripts/layui/src/layuiadmin/layui/css/layui.css"));

            bundles.Add(new StyleBundle("~/bundles/admincss").Include(
                      "~/Scripts/layui/src/layuiadmin/style/admin.css"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                      "~/Scripts/layui/src/layuiadmin/style/login.css"));
#else

            bundles.Add(new ScriptBundle("~/bundles/layui").Include(
                        "~/Scripts/layui/dist/layuiadmin/layui/layui.js"));
            
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Scripts/layui/dist/layuiadmin/layui/css/layui.css"));

            bundles.Add(new StyleBundle("~/bundles/admincss").Include(
                      "~/Scripts/layui/dist/layuiadmin/style/admin.css"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                      "~/Scripts/layui/dist/layuiadmin/style/login.css"));
#endif




        }
    }
}
