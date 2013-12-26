using System.Web;
using System.Web.Optimization;

namespace PassIssueSystem
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Clear all items from the ignore list to allow minified CSS and JavaScript files in debug mode
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.custom.js",
                        "~/Scripts/jquery.mobile.custom.min.js",
                        "~/Scripts/jquery-migrate-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/jquery.timeago.js",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/theme.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/fullcalendar.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/modernizr-{version}.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/wysihtml5.min.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/excanvas.js",
                        "~/Scripts/retina.js",
                        "~/Scripts/flot*"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/Theme/bootbox.min.css",
                        "~/Content/Theme/fullcalender.css",
                        "~/Content/Theme/fullcalender.print.css",
                        "~/Content/Theme/font-awesome.min.css",
                        "~/Content/Theme/light-theme.css",
                        "~/Content/Theme/theme-colors.css",
                        "~/Content/Theme/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/Custom").Include(
                       "~/Scripts/Custom/Request.js"));
        }
    }
}