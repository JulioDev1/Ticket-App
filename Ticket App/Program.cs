using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
builder.Services.AddScoped<IEventRepositories, EventsRepositories>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<ITicketRepositories, TicketRepositories>();
builder.Services.AddScoped<ITicketsService, TicketService>();
builder.Services.AddScoped<IPaymentService, PaymentService>(); 

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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api v1",
        Description = "Admin.Api",

    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name="Authorization",
        Type= SecuritySchemeType.ApiKey,
        Scheme ="Bearer",
        BearerFormat ="JWT",
        In = ParameterLocation.Header,
        Description = "Cabeçalho de autorização jwt"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
