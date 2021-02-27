using Pchp.Core;
using Pchp.Core.Reflection;
using PeachPied.WordPress.Standard;

namespace JokesWebApp.Plugins
{
    //This class takes care of registering the C# DemoWidget plugin.
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