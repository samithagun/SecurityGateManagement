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
                        "~/Scripts/jquery-{version}.min.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.ui.touch-punch.min.js",
                        "~/Scripts/jquery.timeago.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/wysihtml5.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/fullcalender.min.js",
                        "~/Scripts/modernizr-{version}.js",
                        "~/Scripts/modernizr.min.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/flot.min.js",
                        "~/Scripts/excanvas.js",
                        "~/Scripts/retina.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/theme.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/Theme/bootbox.min.css",
                        "~/Content/Theme/bootstrap*",
                        "~/Content/Theme/fullcalender.css",
                        "~/Content/Theme/light-theme.css",
                        "~/Content/Theme/theme-colors.css"));

            bundles.Add(new ScriptBundle("~/bundles/Custom").Include(
                       "~/Scripts/Custom/Request.js"));
        }
    }
}