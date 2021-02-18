using Pchp.Core;
using Peachpie.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using JokesWebApp.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace JokesWebApp.Plugins
{
    //WordPress plugin written in c#. Uses Peachpie WordPress API made available to .NET
    public class DemoWidget : WP_Widget 
    {
        Context ct;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbctx;
        public JokesWebApp.Models.Joke joke;
        public IEnumerable<JokesWebApp.Models.Joke> JokeList { get; set; }
        public PhpValue title { get; set; } = (PhpValue)"Default";

        public DemoWidget(Context ctx) : base(
            "demo_widget",
            "Demo Widget",
            new PhpArray(2) { { "classname", "DemoWidget" }, { "description", "Wordpress widget in c#" } },
            "required"
        )
        {
            this.ct = ctx;
            _logger = ct.CreateScope().ServiceProvider.GetService <ILogger<DemoWidget>>();
            _dbctx = ct.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            _logger.LogInformation("Constructor");
        }

        //Rendering the widget that displays on WordPress page
        public override PhpValue widget(PhpValue args, PhpValue instance)
        {
            _logger.LogInformation("Widget");

            JokeList = _dbctx.Joke.ToList();
            ct.RenderPartial("DemoWidgetView", this);

            PhpValue ret = "";
            return ret;
        }

        //last 2 helper functions render form and update widget
        //they don't do much here
        //have not figured out how to send back data from the view 
        //to fill the PhpValue instance["someProperty"] attribute
        public override PhpValue form(PhpValue instance)
        {
            //WIP
            _logger.LogInformation("Form");

            ct.RenderPartial("DemoWidgetAdmin", this);

            PhpValue ret = "";
            return ret;
        }

        public override PhpValue update(PhpValue new_instance, PhpValue old_instance)
        {
            //WIP
            _logger.LogInformation("Update");
            PhpValue instance = new_instance;
            return instance;
        }
    }
}