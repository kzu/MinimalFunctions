using System.Runtime.InteropServices;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

record Person(string FirstName, string LastName, string Runtime);

record PersonApi(ILogger<Person> Log)
{
    [Function(nameof(GetPerson))]
    public Person GetPerson([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "person")] HttpRequestData req)
    {
        Log.LogInformation("We got DI too :)");
        return new Person("Daniel", "Cazzulino", RuntimeInformation.FrameworkDescription);
    }
}