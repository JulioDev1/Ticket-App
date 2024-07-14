using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ticket_App.Context;
using Ticket_App.Repositories;
using Ticket_App.Repositories.interfaces;
using Ticket_App.Service;
using Ticket_App.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options =>
{

    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDb"));
});
//services
builder.Services.AddScoped<IUserRepository, UserRepostories>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<ITokenService, TokenService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["key"] ?? String.Empty));
var audience = jwtSettings["Audience"];
var issuer = jwtSettings["Issuer"];


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =secretkey,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
