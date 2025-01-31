using Dashclass.Model;
using DashJobs.Components;
using DashJobs.Repository;
using DashJobs.Repository.Candidates;
using DashJobs.Repository.Sessions;
using DashJobs.Repository.Users;
using DashJobs.Services.Candidates;
using DashJobs.Services.Users;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Injeta dependencias de repository
services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(configuration["DB_CONNECTION"]));
services.AddScoped<IBaseRepository<Candidate>, BaseRepository<Candidate>>(); 
services.AddScoped<ICandidatesRepository, CandidatesRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IUserService,UserService>();
services.AddScoped<ICandidatesService, CandidatesService>();
services.AddScoped<ISessionRepository, SessionRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";


app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run($"http://0.0.0.0:{port}");