using System.Web;
using System.Web.Optimization;

namespace SlickCMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // css
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/0_normalize-v2.1.0.css",
                "~/Content/css/1_html5bp.css",
                "~/Content/css/2.1_bootstrap-grid-system.css",
                "~/Content/css/2.2_bootstrap-buttons.css",
                "~/Content/css/3_primary.css",
                "~/Content/css/4_helper.css",
                "~/Content/css/5_mediaqueries.css",
                "~/Content/css/6_print.css",
                "~/Content/css/7_browsers.css",
                "~/Content/css/8_jquery.lightbox-0.5.css"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/js/libraries/modernizr-*"));

            // js
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/js/plugins.js",
                "~/Content/js/libraries/responsiveslides.min.js",
                "~/Content/js/libraries/jquery.lightbox-0.5.min.js",
                "~/Content/js/script.js"
            ));

            // NOTE: kept ckeditor separate for now, as it caused issues when combined/minified - and it might not be required on the frontend (just the admin?)
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include("~/Content/ckeditor/ckeditor.js"));

            // mvc defaults:
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));
            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
        }
    }
}
