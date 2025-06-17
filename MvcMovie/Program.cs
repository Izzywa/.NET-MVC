using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;

var builder = WebApplication.CreateBuilder(args);

// Initialize ReactJS.NET
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();
// make sure a JavaScript engine is registered
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
    .AddV8();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseReact(config =>
{
    // Configure ReactJS.NET
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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
