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

