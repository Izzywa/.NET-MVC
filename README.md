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