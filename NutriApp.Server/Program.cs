using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NutriApp.Server.DataAccess.Context;
using NutriApp.Server.DataAccess.Entities.User;
using NutriApp.Server.Middleware;
using NutriApp.Server.Repositories;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services;
using NutriApp.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// app db context
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("AppDbConnection")
    )
);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

// auth
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// Middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("FrontEndClient", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(
                builder.Configuration["AllowedOrigins"] ??
                throw new InvalidOperationException(
                    "Allowed Origins in appsettings.json not found"
                )
            );
    });
});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Services
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();

// identity endpoints
app.MapIdentityApi<User>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseResponseCaching();
app.UseCors("FrontEndClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();