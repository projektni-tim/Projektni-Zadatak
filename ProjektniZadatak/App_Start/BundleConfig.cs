using System.Web;
using System.Web.Optimization;

namespace ProjektniZadatak
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/validacija.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery-2.2.3.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/app.min.js",
                      "~/Scripts/ion.rangeSlider.min.js",
                      "~/Scripts/bootstrap-slider.js",
                      "~/Scripts/respond.js", 
                      "~/Scripts/validacija.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/ion.rangeSlider.min.css",
                "~/Content/ion.rangeSlider.skinNice.min.css",
                "~/Content/slider.css",
                "~/Content/dataTables.bootstrap.css",
                "~/Content/AdminLTE.min.css",
                "~/Content/_all-skins.min.css",
                "~/Content/bootstrap.min.css",
                "~/Content/dropdown.less" 
                ));
        }
    }
}
