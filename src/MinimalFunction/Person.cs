record Person(string FirstName, string LastName, string Runtime, string Version);

record PersonApi(ILogger<Person> Log)
{
    [FunctionName(nameof(GetPerson))]
    public Person GetPerson([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "person")] HttpRequestMessage req)
    {
        Log.LogInformation("We got DI too :)");
        return new Person("Daniel", "Cazzulino", RuntimeInformation.FrameworkDescription, ThisAssembly.Info.InformationalVersion);
    }
}