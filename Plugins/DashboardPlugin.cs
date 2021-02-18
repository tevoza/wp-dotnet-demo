using Microsoft.AspNetCore.Mvc.ViewEngines;
using Pchp.Core;
using Peachpie.AspNetCore.Mvc;
using PeachPied.WordPress.Standard;

//Example of a dashboard plugin rendering partial view
namespace JokesWebApp.Plugins
{
    public class DashboardPlugin : IWpPlugin
    {
        public string Title { get; } = "Dasboard Widget";
        public DashboardPlugin()
        {

        }

        public void Configure(WpApp app)
        {
            app.DashboardWidget("peachpied.widget.1", "Razor Partial View", writer =>
            {
                app.Context.RenderPartial("DashboardWidget", this);
            });
        }
    }
}
