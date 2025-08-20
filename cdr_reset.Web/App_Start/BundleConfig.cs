using System.Web;
using System.Web.Optimization;

namespace cdr_reset.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/Bootstrap").Include(
                      "~/Scripts/Bootstrap/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/Bootstrap/css").Include(
                      "~/Content/Bootstrap/bootstrap.css",
                      "~/Content/Bootstrap/site.css"));

            bundles.Add(new StyleBundle("~/Content/CSS").Include(
                      "~/Content/CSS/Login.css",
                      "~/Content/CSS/Dashboard.css",
                      "~/Content/CSS/SearchBar.css",
                      "~/Content/CSS/PopupModal.css",
                      "~/Content/CSS/Dropdown.css",
                      "~/Content/CSS/Layout.css"));

            bundles.Add(new StyleBundle("~/Content/FontAwesome").Include(
                      "~/Content/FontAwesome/css/all.css"));
        }
    }
}
