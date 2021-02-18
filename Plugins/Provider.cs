using System;
using System.Collections.Generic;
using System.Linq;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Pchp.Core;
using PeachPied.WordPress.Standard;

namespace JokesWebApp.Plugins
{
    [Export(typeof(IWpPluginProvider))]
    class Provider : IWpPluginProvider
    {
        public IEnumerable<IWpPlugin> GetPlugins(IServiceProvider provider)
        {
            yield return new DashboardPlugin();
            yield return new DemoWidgetPlugin();
            yield return new ExampleWidgetPlugin();
        }

    }
}
