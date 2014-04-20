using System.Web;
using System.Web.Optimization;

namespace MonsterAdmin
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            //Common
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/common/bootstrap.js",
                "~/Scripts/common/bootstrap.datepicker.js",
                "~/Scripts/common/bootstrap.js",
                "~/Scripts/common/select2.js",
                "~/Scripts/common/theme.js",
                "~/Scripts/common/spin.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/form").Include(
                "~/Scripts/common/jquery.uniform.js"
             )); 

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.unobtrusive*",
                        "~/Scripts/jquery/jquery.validate*"));

            //Validation
            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/validation/languages/jquery.validationEngine-en.js", //Validation Engine English
                "~/Scripts/validation/jquery.validationEngine.js" //Validation Engine
                ));

            //Angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/angular/1.2.9/angular.js",
                 "~/Scripts/angular/1.2.9/angular-resource.js",
                 "~/Scripts/angular/1.2.9/angular-route.js",
                 "~/Scripts/angular/1.2.9/angular-sanitize.js",
                 "~/Scripts/angular/angular-bootstrap-select.js",
                 "~/Scripts/angular/apps.js",
                 "~/Scripts/angular/api.js",
                 "~/Scripts/angular/service.js",
                 "~/Scripts/angular/directive.js",
                 "~/Scripts/angular/ui-bootstrap-tpls-0.6.0.js",
                 "~/Scripts/angular/user.js",
                 "~/Scripts/angular/listing.js"
                 ));

            //File uploader
            bundles.Add(new ScriptBundle("~/bundles/upload").Include(
                "~/Scripts/fileupload/vendor/jquery.ui.widget.js",
                "~/Scripts/fileupload/load-image.js",
                "~/Scripts/fileupload/jquery.iframe-transport.js",
                "~/Scripts/fileupload/jquery.fileupload.js",
                "~/Scripts/fileupload/jquery.fileupload-process.js",
                "~/Scripts/fileupload/jquery.fileupload-image.js",
                "~/Scripts/fileupload/jquery.fileupload-validate.js",
                "~/Scripts/fileupload/jquery.fileupload-angular.js",
                "~/Scripts/fileupload/app.js"
                ));

            //Listing e.g- ck editor, file uploader
            bundles.Add(new ScriptBundle("~/bundles/formplugin").Include(
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/select2/select2.js"
                ));

            //Bootstrap
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap/bootstrap.css",
                        "~/Content/bootstrap/bootstrap.datepicker.css",
                        "~/Content/bootstrap/font-awesome.css"
                        ));

            //Admin Themes
            bundles.Add(new StyleBundle("~/Content/themes/admin/css").Include(
                        "~/Content/common.css",
                        "~/Content/themes/admin/bootstrap-overrides.css",
                        "~/Content/themes/admin/libraries/uniform.default.css",
                        "~/Content/themes/admin/libraries/select2.css",
                        "~/Content/themes/admin/libraries/select2-bootstrap.css",
                        "~/Content/themes/admin/libraries/validationEngine.jquery.css",
                        "~/Content/themes/admin/global/layout.css",
                        "~/Content/themes/admin/global/elements.css",
                        "~/Content/themes/admin/global/icons.css",
                        "~/Content/themes/admin/global/table.css",
                        "~/Content/themes/admin/module/form-showcase.css",
                        "~/Content/themes/admin/module/gallery.css",
                        "~/Content/fileupload/jquery.fileupload-ui.css",
                        "~/Content/fileupload/jquery.fileupload.css"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}