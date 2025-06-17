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
