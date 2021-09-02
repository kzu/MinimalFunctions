using System.Runtime.InteropServices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

record Person(string FirstName, string LastName, string Runtime);

record PersonApi(ILogger<Person> Log)
{
    [FunctionName(nameof(GetPerson))]
    public Person GetPerson([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "person")] HttpRequestMessage req)
    {
        Log.LogInformation("We got DI too :)");
        return new Person("Daniel", "Cazzulino", RuntimeInformation.FrameworkDescription);
    }
}