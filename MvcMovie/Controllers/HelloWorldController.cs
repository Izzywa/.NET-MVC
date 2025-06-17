using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

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
    // Requires using System.Text.Encodings.Web;
    public string Welcome(string name = "Stranger", int numTimes = 1)
    {
        return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
    }

    public string GetId(int id = 1, string name = "Stranger")
    {
        return HtmlEncoder.Default.Encode($"You selected id: {id}, {name}");
    }
}