using Microsoft.EntityFrameworkCore;
using Visitor.Cfg.Infrastructure.Cache;
using VisitorService.aplication.Interface.Cache;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.Settings;
using VisitorService.Application.UseCases;
using VisitorService.Domain.Interfaces;
using VisitorService.Domain.Services;
using VisitorService.Infrastructure.Context;
using VisitorService.Infrastructure.Repositories;
using VisitorService.Infrastructure.Services;
using VisitorService.Interfaces.Extensions;
using VisitorService.Interfaces.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=visitorservice.db"));

// Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();
builder.Services.AddTransient<IEmailService, EmailService>();

// Handlers / Use Cases
builder.Services.AddScoped<IcreateVisitHandler, CreateVisitHandler>();
builder.Services.AddScoped<IGetAllVisitsHandler, GetAllVisitsHandler>();
builder.Services.AddScoped<IGetTodayApprovedVisitsHandler, GetTodayApprovedVisitsHandler>();
builder.Services.AddScoped<IloginHandler, LoginHandler>();
builder.Services.AddScoped<IRegisterVisitorHandler, RegisterVisitorHandler>();
builder.Services.AddScoped<IUpdateVisitStatusHandler, UpdateVisitStatusHandler>();
builder.Services.AddScoped<IVisitCheckInHandler, VisitCheckInHandler>();
builder.Services.AddScoped<IVisitCheckOutHandler, VisitCheckOutHandler>();

// Email Settings
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings")
);

// JWT Auth
builder.Services.AddJwtAuthentication(builder.Configuration);

// Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Exceptions
app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

// Map Controllers
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
