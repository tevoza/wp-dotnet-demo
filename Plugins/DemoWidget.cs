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
    public class DemoWidget : WP_Widget
    {
        Context ct;
        public DemoWidget(Context ctx) : base(
            "demo_widget",
            "Demo Widget",
            new PhpArray(2) { { "classname", "DemoWidget" }, { "description", "Wordpress widget in dotnet" } },
            "yeet"
        )
        {
            this.ct = ctx;
        }

        public string Title { get; } = "Demo Widget";

        public override PhpValue widget(PhpValue args, PhpValue instance)
        {
            WP_Hook Hook = new WP_Hook();
            PhpValue title = Hook.apply_filters("widget_title", instance["title"]);
            ct.RenderPartial("DemoWidget", this);
            
            return title;
        }

        public override PhpValue form(PhpValue instance)
        {
            PhpValue title;
            title = "woa";

            return title;
        }

        public override PhpValue update(PhpValue new_instance, PhpValue old_instance)
        {
            PhpValue instance = old_instance;
            //instance["title"] =  strip_tags
            return instance;
        }
    }
}