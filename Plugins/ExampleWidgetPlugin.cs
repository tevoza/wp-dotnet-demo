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
    public class ExampleWidgetPlugin : IWpPlugin
    {
        //Regsitering a widget "jpen_Example_Widget" which is defined in php.
        //Shows how existing php can be called and used within c#
        //After building the PhpLib, the class implementation becomes available here.
        WpApp app;
        public delegate int Del();
        public int RegisterMyWidget()
        {
            var tinfo = PhpTypeInfoExtension.GetPhpTypeInfo<jpen_Example_Widget>();
            this.app.Context.DeclareType(tinfo, "jpen_Example_Widget");
            this.app.Context.Call("register_widget", "jpen_Example_Widget");
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