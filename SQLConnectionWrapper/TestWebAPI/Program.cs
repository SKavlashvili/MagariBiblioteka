using NpgSQLConnectionWrapper;
using TestWebAPI.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Adding DB contexts in services as scoped
builder.Services.AddDBServices(typeof(Test2Context).Assembly, typeof(Test2Context), typeof(TestContext));


var app = builder.Build();


//Adding connection cutter
app.UseConnectionCutter();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());


app.Run();
