# WordPress to .Net
WP-to-dotnet Investigation, making use of the <code>PeachPied.WordPress.AspNetCore</code> package within the <code>.NET Core</code> framework.

# Build Project.
1. Clone from github: <code>git clone https://github.com/tevoza/wp-dotnet-demo</code>  
3. Add nuget feed: <code>dotnet nuget add source https://feed.peachpie.io/wpdotnet/v3/index.json -n "peachpie.io wpdotnet"</code>
2. MySQL database(required for WordPress):  
I used docker because it is quick and fast. Don't have to.  
<code>docker run --name=wp_dotnet -p 3306:3306 -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=wordpress mysql --default-authentication-plugin=mysql_native_password</code>  
4. Then, in <code>Startup.cs</code>, a few important configuration settings. to note:
~~~c#
    public void ConfigureServices(IServiceCollection services)
    {
    ...
        services.AddWordPress(options =>
        {
            //MySQL database connection
            options.DbHost = "192.168.1.16";//I used a different machine, can use localhost
            options.DbPassword = "password";
            options.DbName = "wordpress";

            //explicitely set url, or wordpress gets confused.
            //for example starting with kestrel vs iis, change the port accordingly
            // "/content" is used to demonstrate wordpress being used next to MVC application
            options.HomeUrl = "https://localhost:5001/content";
            options.SiteUrl = "https://localhost:5001/content";

            //Plugins written or registered in C#
            options.PluginContainer.Add(new DashboardPlugin());
            options.PluginContainer.Add(new DemoWidgetPlugin());
            ...  
        });
    }
    ...
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        ...
        //reroutes wordpress to /content
        app.Map(new PathString("/content"), wordpressApp =>
        {
            wordpressApp.UseWordPress();
        });
        //if undesired, remove the above and uncomment below
        //app.UseWordPress();
        ...
    }
    ...
~~~

# 1. Get a demo version running (status)
Basic Wordpress site running on .Net  
Looking towards extension and integration with other .NET pages and plugin integration

# 2. PeachPied investigation:
1. - [x] **Are there any similar projects out there?**  
    *Not really. At least no other compilers existed for compiling php into the .NET runtime. The compiler is a continuation of the now abandoned Phalanger compiler.*
2. - [x] **Who are their clients?**  
    *Companies looking for to integrate old-style wordpress with a modern, powerful environment(.NET)*
3. - [x] **What’s the pricing and commercial agreement implications re using PeachPied?**  
    *The package is free. It makes use of the Apache 2.0 licence which allows liberal use of the software, including the ability to make changes.*  
4. - [x] **Do we have access to the PeachPied source code?**  
    *Yes :D Here: https://github.com/iolevel/wpdotnet-sdk*
5. - [x] **How well does PeachPied work? i.e**  
    - **Is it easy to setup?**  
        *Yes. It does require a MySQL database. For this I experimented with docker for database setup. Docker is nice because it neatly containerizes your database. Starting the docker instance is as simple as <code>docker run --name=mysql1 -p 3306:3306 -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=wordpress mysql --default-authentication-plugin=mysql_native_password</code>. Although I believe Azure provides a neatly integrated MySQL solution, which is probably better.*
    - **Is it easy to use?**  
        *Yes, a few function calls in <code>Startup.cs</code>*    
    - **How well does it work across different WP modules?**  
6. - [x] **What version of .Net and frameworks are supported?**  
    *.NET Core 3.0 and newer*  
7. - [ ] **What limitation exists?**  
8. - [x] **What are the great wins of using this technology?**  
    *Wordpress can be simply built as a <code>.dll</code> file, which reduces weak points of entry(enchancing security. The capability to run within the .NET ecosystem is a massive win.*
# 3. .Net / C# investigation:
1. - [x] **Demonstrate the build of a complete page within C# and MVC, making use of SQL, Azure, etc.**  
    *Wrote a CRUD application for retrieving, editing and viewing a collection of jokes from a MS SQL database*  
2. - [x] **Demonstrate the enhancement of an existing WP page, making use of C#**  
   *Wrote a simple demo dashboard plugin with C# adding a custom widget. Still experimenting with more options.*
3. - [x] **Explore the options in terms of WP themes and WP modules implementations e.g. WooCommerce**  
    *Precompiles themes can easily be installed as NuGet packages from Visual Studio. If they are not available in the repository, they can still be manually built in a .NET wordpress application by dropping them into the wordpress plugin directory.*  
4. - [ ] **Explore how a module e.g. WooCommerce may updated / extended making use of C# coding**  

# 4. WP investigation (once converted to .Net):
1. - [x] **Demonstrate the build of an existing page using standard WP builders and tools**  
    *A simple demo website has been built for demonstration purposes, using standard WordPress menus.*  
2. - [x] **Demonstrate the implementation of standard WP modules**  
    *If the module already exists as a NuGet package, it can be installed simply through NuGet in Visual Studio, and is a more secure signed solution. Dropping the module in the WordPress plugin directory is an alternative option if a NuGet package is not available.*  
3. - [x] **Demonstrate the upgrade of WP**  
   *<code>PeachPied.WordPress.AspNetCore</code> can be upgraded through NuGet in Visual Studio. It must be noted that any precompiled plugins(installed with NuGet) must also be matched to the current version of <code>PeachPied.WordPress</code>*
# 5. General / Important observations and findings

# 6. Produce a report based on all outcomes covered in project

# Catchup 2  
1. Wordpress running in MVC app. Switching between mvc pages and wordpress pages.  
2. Enhancing wordpress pages with c#  
- Widget plugin in c# to be rendered on wordpress pages.  
- Wordpress API is available in C# for this purpose. But there is zero documentation on implementing wordpress plugins in c#.
- To do this, I had to refer to wordpress documentation and try to recreate PHP plugins in .NET. I beleieve it is possible with more wordpress experience.
3. WooCommerce
- I have not succefully built woocommerce yet. I believe I should be able to get it working
4. Extension of php plugins in C#
- I have not explored this yet. I've seen it being achieved conceptually though, referencing php classes in c#.

#requried
- widget on page
- code behind widget

- php plugins to 
- build themes from php

# Catchup 3  
- Woocommerce  
   Managed to contact peachpie maintainers enquiring about building woocommerce. https://gitter.im/iolevel/peachpie  
   Managed to run the plugin(as per the suggestions), built in php, after removing a few php source files. UI was glitchy and seemingly unusable. 
   VS fails to load the project due to very long file names when building the project. Have to execute through command line.
   
- Php Themes and plugins
   Succesfully installed a few demo plugins and a themes(in php) Demonstrating it is possible.  
   Further investigation: building own themes and plugins as nuget packages. Should be viable.
   
- Widget on page  
   Using shortcode plugin, widgets can be added anywhere on the page.  
   
- Code behind the view.  
   Struggling to implement page model object for rendering code-behind the view.  
