using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using JokesWebApp.Data;
using JokesWebApp.Models;

namespace JokesWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<ApplicationDbContext>();
                    var jokes = db.Joke.ToList();
                    if (!jokes.Any())
                    {
                        System.Console.WriteLine("No data found. Adding records");
                        var rec1 = new Joke { 
                            JokeQuestion = "Why did the chicken cross the road?",
                            JokeAnswer = "To Get to the other side."
                        }; 
                        var rec2= new Joke { 
                            JokeQuestion = "What's the capital of China?",
                            JokeAnswer = "France!"
                        }; 
                        db.Joke.Add(rec1);
                        db.Joke.Add(rec2);
                        db.SaveChanges();
                    }
                    else
                    {
                        System.Console.WriteLine("Already Contains data!");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
