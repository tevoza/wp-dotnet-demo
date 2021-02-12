using Pchp.Core;
using Pchp.Core.Reflection;
using PeachPied.WordPress.Standard;
using PeachPied.WordPress;
using PeachPied.WordPress.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Peachpie.AspNetCore.Mvc;
using Peachpie.AspNetCore.Web;
using Peachpie;
using System;
using JokesWebApp.Models;
using System.Collections.Generic;
using System.Collections;

namespace JokesWebApp.Plugins
{
    public class DemoWidget : WP_Widget
    {
        Context ct;
        public IEnumerable<JokesWebApp.Models.Joke> JokeList { get; set; }

        public DemoWidget(Context ctx) : base(
            "demo_widget",
            "Demo Widget",
            new PhpArray(2) { { "classname", "DemoWidget" }, { "description", "Wordpress widget in dotnet" } },
            "idk"
        )
        {
            this.ct = ctx;
            //get a list of jokes objects from JokesController?
            //JokeList = await
        }

        public string Title { get; } = "Demo Widget";

        public override PhpValue widget(PhpValue args, PhpValue instance)
        {
            WP_Hook Hook = new WP_Hook();
            PhpValue title = Hook.apply_filters("widget_title", instance["title"]);
            ct.RenderPartial("DemoWidget", this);
            //return RedirectResult("/Home/Privacy");
            
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