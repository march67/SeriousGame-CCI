using APIRest_2D_interface_project.Business.Services.Implementations;
using APIRest_2D_interface_project.Business.Services.Interfaces;
using APIRest_2D_interface_project.DataAccess.Context;
using APIRest_2D_interface_project.DataAccess.Repositories.Implementations;
using APIRest_2D_interface_project.DataAccess.Repositories.Interfaces;
using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Infrastructure.Extensions;
using APIRest_2D_interface_project.Infrastructure.Mappings.Resolvers;
using APIRest_2D_interface_project.Infrastructure.Services.Implementations;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));

// Others
builder.Services.AddScoped<User>();
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValueResolver<UserRegisterRequestDTO, User, string>, PasswordHashResolver>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Mapping Configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// JWT Configuration
builder.Services.AddJwtAuthentication(builder.Configuration);

// HTTP Pipeline
var app = builder.Build();

// Middlewares configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Routage and security
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Execution
app.Run();
