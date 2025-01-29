using Dashclass.Model;
using DashJobs.Components;
using DashJobs.Repository;
using DashJobs.Repository.Candidates;
using DashJobs.Repository.Session;
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
services.AddScoped<ISessionRepository, SessionRepository>();
services.AddScoped<IUserService,UserService>();
services.AddScoped<ICandidatesService, CandidatesService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
