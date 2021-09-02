var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureFunctionsWorkerDefaults();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.MapGet("/", () => "Hello World!");

app.Run();