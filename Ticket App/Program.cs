using Microsoft.EntityFrameworkCore;
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
