using Pchp.Core;
using Pchp.Core.Reflection;
using PeachPied.WordPress.Standard;
using PeachPied.WordPress;
using PeachPied.WordPress.AspNetCore;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Peachpie.AspNetCore.Mvc;
using Peachpie.AspNetCore.Web;
using Peachpie;

namespace JokesWebApp.Plugins
{
    public class DemoWidgetPlugin : IWpPlugin
    {
        WpApp app;
        public delegate int Del();
        public int RegisterMyWidget()
        {
            var tinfo = PhpTypeInfoExtension.GetPhpTypeInfo<DemoWidget>();
            this.app.Context.DeclareType(tinfo, "DemoWidget");
            this.app.Context.Call("register_widget", "DemoWidget");
            return 0;
        }

        public void Configure(WpApp app)
        {
            this.app = app;
            Del MyDel = RegisterMyWidget;
            var routineInfo = RoutineInfo.CreateUserRoutine("RegisterMyWidget", MyDel);
            app.Context.Call("add_action", (PhpValue)"widgets_init", routineInfo);
        }
    }
}
