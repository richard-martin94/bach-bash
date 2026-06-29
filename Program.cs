using bach_bash.Endpoints;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using bach_bash.Persistence;
using bach_bash.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BashDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IBashService, BashService>();
builder.Services.AddTransient<IBasherService, BasherService>();
builder.Services.AddTransient<IBashMemberService, BashMemberService>();
builder.Services.AddTransient<IChallengeService, ChallengeService>();
builder.Services.AddTransient<ISubmissionService, SubmissionService>();

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});*/

var app = builder.Build();

await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<BashDbContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();

app.MapBashEndPoints();
app.MapBasherEndPoints();
app.MapBashMemberEndPoints();
app.MapChallengeEndpoints();
app.MapSubmissionEndpoints();

app.MapGet("/", () => "The negotiator").Produces(200, typeof(String));


app.Run();
