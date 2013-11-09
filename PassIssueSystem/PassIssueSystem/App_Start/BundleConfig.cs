using System.Web;
using System.Web.Optimization;

namespace PassIssueSystem
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/wysihtml5.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-daterangepicker.js",
                        "~/Scripts/bootstrap-wysihtml5.js",
                        "~/Scripts/fullcalender.js",
                        "~/Scripts/modernizr.min.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/flot.min.js",
                        "~/Scripts/excanvas.js",
                        "~/Scripts/retina.js",
                        "~/Scripts/theme.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/Theme/bootbox.min.css",
                        "~/Content/Theme/bootstrap.css",
                        "~/Content/Theme/bootstrap-daterangepicker.css",
                        "~/Content/Theme/bootstrap-wysihtml5.css",
                        "~/Content/Theme/fullcalender.css",
                        "~/Content/Theme/light-theme.css",
                        "~/Content/Theme/theme-colors.css"));
        }
    }
}