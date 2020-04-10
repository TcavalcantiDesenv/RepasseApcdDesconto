using System.Web;
using System.Web.Optimization;

namespace PlatinDashboard.Presentation.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/unobstrusive").Include(
                        "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/coreui/icons/css").Include(
                      "~/coreuilib/coreui/icons/css/coreui-icons.min.css",
                      "~/coreuilib/flag-icon-css/css/flag-icon.min.css",
                      "~/coreuilib/font-awesome/css/font-awesome.min.css",
                      "~/coreuilib/simple-line-icons/css/simple-line-icons.css"));

            bundles.Add(new StyleBundle("~/coreui/mainstyle/css").Include(
                      "~/Content/css/style.css",
                      "~/coreuilib/pace-progress/css/pace.min.css"));

            bundles.Add(new ScriptBundle("~/coreui/plugins/js").Include(
                      "~/coreuilib/jquery/js/jquery.min.js",
                      "~/coreuilib/popper.js/js/popper.min.js",
                      "~/coreuilib/bootstrap/js/bootstrap.min.js",
                      "~/coreuilib/pace-progress/js/pace.min.js",
                      "~/coreuilib/perfect-scrollbar/js/perfect-scrollbar.min.js",
                      "~/coreuilib/coreui/coreui-pro/js/coreui.min.js"));

            bundles.Add(new ScriptBundle("~/coreui/charts/js").Include(
                      "~/coreuilib/chart.js/js/Chart.min.js",
                      "~/coreuilib/coreui/coreui-plugin-chartjs-custom-tooltips/js/custom-tooltips.min.js"));

            bundles.Add(new StyleBundle("~/coreui/datatables/css").Include(
                      "~/coreuilib/datatables/css/dataTables.bootstrap4.css"));

            bundles.Add(new ScriptBundle("~/coreui/datatables/js").Include(
                      "~/coreuilib/datatables/js/jquery.dataTables.min.js",
                      "~/coreuilib/datatables/js/dataTables.bootstrap4.js"));

            bundles.Add(new StyleBundle("~/coreui/ladda/css").Include(
                      "~/coreuilib/ladda/css/ladda-themeless.min.css"));

            bundles.Add(new ScriptBundle("~/coreui/ladda/js").Include(
                    "~/coreuilib/ladda/js/spin.min.js",
                    "~/coreuilib/ladda/js/ladda.min.js"));

            bundles.Add(new ScriptBundle("~/coreui/file-input/script").Include(
                      "~/coreuilib/file-input/js/sortable.min.js",
                      "~/coreuilib/file-input/js/fileinput.min.js",
                      "~/coreuilib/file-input/js/purify.min.js",
                      "~/coreuilib/file-input/js/canvas-to-blob.min.js",
                      "~/coreuilib/file-input/js/pt-BR.js"));

            bundles.Add(new StyleBundle("~/coreui/file-input/css").Include(
                      "~/coreuilib/file-input/css/fileinput.min.css"));

            bundles.Add(new ScriptBundle("~/coreui/select2/script").Include(
                      "~/coreuilib/select2/js/select2.min.js"));

            bundles.Add(new StyleBundle("~/coreui/select2/css").Include(
                      "~/coreuilib/select2/css/select2.min.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
