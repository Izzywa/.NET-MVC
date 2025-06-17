# Model Binding
Controllers work with data from HTTP requests
- route data may provide values for the properties of the model

model binding automates the process:
- retrieves data from various sources
    - route data
    - form fields
    - query strings
- provides the data to controllers in method parameters and public properties
- converts string data to .NET types
- updates properties of complex types

## example 
```C#
[HttpGet("{id}")]
public ActionResult<Pet> GetById(int id, bool dogsOnly)
```
the app receives a request with this URL
```HTTP
https://contoso.com/api/pets/2?DogsOnly=true
```
steps after routing system selects the action method
- Finds the first parameter of `GetById`, an integer named `id`
- looks through the available sources in the HTTP request
    - finds `id` = "2" in route data
- converts the string "2" into int 2
- finds the next parameter, `dogsOnly`
- looks through the sources 
    - finds "DogsOnly=true" in the query string
    - name matching is not case sensitive
- converts the string "true" into boolean `true`

framework then calls the `GetById` method, passing the parameters

property is successfully bound => model validation of that property

ControllerBase.ModelState or PageModel.ModelState stores the record of
- what data is bound to the model
- any binding or validation errors

app checks the ModelState.IsValid flag = find out if the process was successful