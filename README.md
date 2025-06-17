# .NET-MVC

## create a web app

```bash
dotnet new mvc -o MvcMovie
code -r MvcMovie
```
- `code -r` opens the folder in the current instance of VS Code

## run the app

```bash
dotnet dev-certs https --trust
```

in vs code run the app without debugging
- windows = `ctrl` + `f5`
- macOS = `^` + `f5`

VS Code:
- starts Kestrel
- launches a browser
- navigatets to `https://localhost:<port#>`

launching the app without debuggin allows you to:
- make code changes
- save the file
- quickly refresh the browser and see the code changes

close the browser windwo

stop the code
- Run menu > stop debugging
- `shift` + `f5`

# add a controller to an ASP.NET Core MVC app
MVC architecureal pattern separates app into:
- Model
- View
- Controller

helps create apps that are more testable and easier to update

MVC-based apps contain:
- Models:
    - classes that represent the data of the app
    - uses validation logic to enforce business rules for that data 
    - retrieve and store model state in a database
    - the model retrieves data from a database => provides it to the view or updates it
    - updated data is written to the database
- Views:
    - components that display the app's UI
    - the UI displays the model data
- Controllers: classes that
    - handle browser requests
    - retrieve model data
    - call view templates that returns a response

MVC app:
- view = displays information
- controller = handles and responds to user input and interaction
    - ex: handles URL segments and query-string values
    - passes the values to the model
    - model might use the values to query the database

MVC architectural pattern:
- separates app into 3 main groups of components
    - Models
    - Views
    - Controller
- helps to achiever separation of concerns:
    - UI logic in the view
    - input logic in the controller
    - business logic in the model
- separation = manage complexity when building an app
    - enables work on one aspect of the implementation at a time without impacting the code of another
    - ex: can work on the view code without depending on the business logic

## add a controller 
add new file in `Controller` folder

```C#
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    public string Index()
    {
        return "This is my default action...";
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public string Welcome()
    {
        return "This is the Welcome action method...";
    }
}
```
- every `public` method in a controller = callable as an HTTP endpoint

HTTP endpoint:
- a targetable URL in the web app
- combines
    - the protocol used: `HTTPS`
    - the network location of the webserver
        - including the TCP port: `localhost:5001`
    - the target URI: `HelloWorld`

MVC invokes controller classes
- and the action methods within them depending on the incoming URL
- default URL routing logic, uses format like this to determine what code to invoke
```
/[Controller]/[ActionName]/[Parameter]
```

routing format in the `Program.cs` file
```C#
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```
browse app and don't supply URL segment = defaults to the "Home" controller + "Index" methods

the preceding URL segments
- first URL segment = determines the controller class to run
    - `localhost:5001/HelloWorld` => HelloWorld controller class
- second part of URL segment = the action method on the class
    - `localhost:5001/HelloWorld/Index` = `Index` action of the `HelloWorldController` class to run
- third part of the URL segment (`id`) = route data

passing parameter information from the URL to the controller
```C#
// GET: /HelloWorld/Welcome/ 
// Requires using System.Text.Encodings.Web;
public string Welcome(string name, int numTimes = 1)
{
    return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
}
```
- uses `HtmlEncoder.Default.Encode` 
    - protect the app from malicious input (such as through Js)

    
```HTTP
http://localhost:5062/HelloWorld/Welcome?name=Rick&numtimes=4
```
- the URL segment `Parameters` isn't used
- `name` and `numTimes` parameters are passed in the query string
- the `?` is a separator, the query string follows
- the `&` character separates field-value pairs

the MVC [model binding](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-9.0) system automatically maps the named parameters from the query to string to parameters in the method

replace the `Welcome` method with 
```C#
public string Welcome(string name, int ID = 1)
{
    return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
}
```
run the following URL
```HTTP
https://localhost:{PORT}/HelloWorld/Welcome/3?name=Rick
```
- the third URL segment mathced the route parameter `id` as defined in the routing template in the `Program.cs` file
    - the trailing `?` in `id?` indicates the `id` parameter is optional
- the `Welcome` method contains parameter `id` that matched the URL template in the `MappControllerRoute` method
- the trailing `?` starts the query string

# add a view to an ASP.NET Core MVC app
 change the controller `HelloWorldController` `Index` method into:
 ```C#
 public IActionResult Index()
{
    return View();
}
 ```
 - calls the controller's View method
 - uses a view template to generate an HTML response

 controller methods:
 - referred to as action methods
    - ex: the `Index` action method 
    - return an `IActionResult` or a class derived from ActionResult

## add a view
- add a new folder _Views.HelloWorld_
- add a new file into the folder, named `Index.cshtml`

```CSHTML
@{
    ViewData["Title"] = "Index";
}

<h2>Index</h2>

<p>Hello from our View Template!</p>
```

## tutorial is using Razor and I don't want to use it
[Tutorial ReactJS.NET](https://reactjs.net/tutorials/aspnetcore.html)
- install NuGet package "React.AspNet"
- install a JS engine
    - `JavaScriptEngineSwithcer.V8`
- install the native assembly based on your architecture and engine choice
    - `JavaScriptEngineSwitcher.V8.Native.win-x64`
- install `JavaScriptEngineSwitcher.Extensions.MsDependencyInjection`

### modify the main .cs file
initialize ReactJS.NET

```C#
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
```

above the 
```C#
services.AddControllersWithViews();
```
add
```C#
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddReact();

// Make sure a JS engine is registered, or you will get an error!
services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
  .AddV8();
```

above the code 
```C#
app.UseStaticFiles();
```
add
```C#
// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
  // If you want to use server-side rendering of React components,
  // add all the necessary JavaScript files here. This includes
  // your components as well as all of their dependencies.
  // See http://reactjs.net/ for more information. Example:
  //config
  //  .AddScript("~/js/First.jsx")
  //  .AddScript("~/js/Second.jsx");

  // If you use an external build too (for example, Babel, Webpack,
  // Browserify or Gulp), you can improve performance by disabling
  // ReactJS.NET's version of Babel and loading the pre-transpiled
  // scripts. Example:
  //config
  //  .SetLoadBabel(false)
  //  .AddScriptWithoutTransform("~/js/bundle.server.js");
});
```
on top of `Views\_ViewImports.cshtml`
```CSHTML
@using React.AspNet
```

### create basic controller and view
Create the `HomeController.cs` file in the `Controller` folder

Create the `Views` > `Home` folder
- add `Index.cshtml` view file
```CSHTML
@{
    Layout = null;
}
<html>
<head>
  <title>Hello React</title>
</head>
<body>
    <div id="content"></div>
    <script crossorigin src="https://cdnjs.cloudflare.com/ajax/libs/react/16.13.0/umd/react.development.js"></script>
    <script crossorigin src="https://cdnjs.cloudflare.com/ajax/libs/react-dom/16.13.0/umd/react-dom.development.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/remarkable/1.7.1/remarkable.min.js"></script>
<script src="@Url.Content("~/js/Tutorial.jsx")"></script>
</body>
</html>
```
- in a real ASP.NET MVC site, you'd use a layout
- this tutorial keeps it simple, keep all HTML in one view file

create a referenced JS file (`tutorial.jsx`)
`wwwroot\js` > add `tutorial.jsx`
- JS code will be written in this file
