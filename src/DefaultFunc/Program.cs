using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
    })
    .ConfigureLogging(l =>
    {
    })
    .Build();

await host.RunAsync();

[Function("GetPerson")]
Person GetPerson([HttpTrigger(AuthorizationLevel.Anonymous, "GET", "/person")] HttpRequestData req, FunctionContext context) 
    => new Person("Daniel", "Cazzulino");

record Person(string FirstName, string LastName);