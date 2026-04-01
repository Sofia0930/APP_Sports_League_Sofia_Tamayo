using Microsoft.EntityFrameworkCore;

using SportsLeague.DataAccess.Context;

using SportsLeague.DataAccess.Repositories;

using SportsLeague.Domain.Interfaces.Repositories;

using SportsLeague.Domain.Interfaces.Services;

using SportsLeague.Domain.Services;

using SportsLeague.Domain.Interfaces.Repositories; // Add for sponsor interfaces
using SportsLeague.DataAccess.Repositories; // Add for sponsor repo
using SportsLeague.Domain.Interfaces.Services; // Add for sponsor service
using SportsLeague.Domain.Services;


var builder = WebApplication.CreateBuilder(args);


// ── Entity Framework Core ──

builder.Services.AddDbContext<LeagueDbContext>(options =>

options.UseSqlServer(

builder.Configuration.GetConnectionString("DefaultConnection")));


// ── Repositories ──

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddScoped<IRefereeRepository, RefereeRepository>(); // NUEVO

builder.Services.AddScoped<ITournamentRepository, TournamentRepository>(); // NUEVO

builder.Services.AddScoped<ITournamentTeamRepository, TournamentTeamRepository>(); // NUEVO
builder.Services.AddScoped<ISponsorRepository, SponsorRepository>();
builder.Services.AddScoped<ITournamentSponsorRepository, TournamentSponsorRepository>();



// ── Services ──

builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IRefereeService, RefereeService>(); // NUEVO

builder.Services.AddScoped<ITournamentService, TournamentService>(); // NUEVO
builder.Services.AddScoped<ISponsorService, SponsorService>();


// ── AutoMapper ──

builder.Services.AddAutoMapper(typeof(Program).Assembly);


// ── Controllers ──

builder.Services.AddControllers();

// ── Swagger ──
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Middleware Pipeline ──
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
