record Person(string FirstName, string LastName, string Runtime, string Version);

record PersonApi(ILogger<Person> Log)
{
    /// <summary>
    /// Gets a person.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="name">Name of the person to retrieve.</param>
    /// <param name="context"></param>
    /// <returns>The person or <see langword="null">null</see>.</returns>
    [FunctionName(nameof(GetPerson))]
    public Person GetPerson([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "person/{name?}")] HttpRequestMessage req, 
        string? name,
         Microsoft.Azure.WebJobs.ExecutionContext context)
    {
        Log.LogInformation("We got DI too :)");
        return new Person(name ?? "Daniel", "Cazzulino", RuntimeInformation.FrameworkDescription, ThisAssembly.Info.InformationalVersion);
    }
}