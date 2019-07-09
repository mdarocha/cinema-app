using React;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CinemaApp.ReactConfig), "Configure")]

namespace CinemaApp
{
	public static class ReactConfig
	{
		public static void Configure()
		{
            JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
            JsEngineSwitcher.Current.EngineFactories.AddV8();

            ReactSiteConfiguration.Configuration
                .AddScript("~/Scripts/cinema/place_selector.jsx");
		}
	}
}