using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace JokesWebApp.Views.Shared
{
    public class DemoWidgetViewModel : PageModel //This object is always null when I try to make use its interface. I don't know how or why
    {
        public string Message { get; private set; } = "PageModel in C#";
        public DemoWidgetViewModel()
        {
            Message = "Woahj!";

        }
    }
}
