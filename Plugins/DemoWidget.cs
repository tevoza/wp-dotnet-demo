using Pchp.Core;
using Pchp.Core.Reflection;
using PeachPied.WordPress.Standard;
using PeachPied.WordPress;
using PeachPied.WordPress.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Peachpie.AspNetCore.Mvc;
using Peachpie.AspNetCore.Web;
using Peachpie;
using System;
using JokesWebApp.Models;
using System.Collections.Generic;
using System.Collections;
using JokesWebApp.Controllers;
using JokesWebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;
using JokesWebApp.Plugins;

namespace JokesWebApp.Plugins
{
    //WordPress plugin written in c#. Uses Peachpie WordPress API made available to .NET
    public class DemoWidget : WP_Widget 
    {
        Context ct;
        public IEnumerable<JokesWebApp.Models.Joke> JokeList { get; set; }

        public DemoWidget(Context ctx) : base(
            "demo_widget",
            "Demo Widget",
            new PhpArray(2) { { "classname", "DemoWidget" }, { "description", "Wordpress widget in dotnet" } },
            "required"
        )
        {
            this.ct = ctx;
        }

        public string Title { get; } = "Demo Widget";
        public string Joke { get; set;  } = "Joke";
        public string JokeAnswer { get; set;  } = "Answer";

        //Rendering the widget that displays on WordPress page
        public override PhpValue widget(PhpValue args, PhpValue instance)
        {
            WP_Hook Hook = new WP_Hook();
            PhpValue title = Hook.apply_filters("widget_title", instance["title"]);
            using (var scope = ct.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                JokeList = db.Joke.ToList();
            }

            ct.RenderPartial("DemoWidgetView", this);
            return title;
        }

        public override PhpValue form(PhpValue instance)
        {
            //WIP
            PhpValue title = "incomplete";
            ct.RenderPartial("DemoWidgetAdmin", this);
            return title;
        }

        public override PhpValue update(PhpValue new_instance, PhpValue old_instance)
        {
            //WIP
            PhpValue instance = old_instance;
            return instance;
        }
    }
}