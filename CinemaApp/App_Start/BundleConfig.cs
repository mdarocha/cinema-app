using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace CinemaApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app")
                        .Include("~/dist/jquery.bundle.js")
                        .Include("~/dist/app.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/reservationFlow")
                        .Include("~/dist/reservationFlow.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/dayPicker")
                        .Include("~/dist/dayPicker.bundle.js"));
        }
    }
}
