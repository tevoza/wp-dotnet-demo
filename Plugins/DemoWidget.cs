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
    public class DemoWidget : WP_Widget //WordPress plugin written in c#
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

            //var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=aspnet-JokesWebApp-E055A851-315E-4C91-9F78-617BF5325ED4")
            //    .Options;

            //using var context = new ApplicationDbContext(contextOptions);

            //JokeList = context.Joke.ToList();
            //System.Diagnostics.Debug.WriteLine("Widget created");
        }

        public string Title { get; } = "Demo Widget";

        public override PhpValue widget(PhpValue args, PhpValue instance)
        {
            WP_Hook Hook = new WP_Hook();
            PhpValue title = Hook.apply_filters("widget_title", instance["title"]);
            System.Diagnostics.Debug.WriteLine("Widget created");
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