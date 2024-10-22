using Authentication_Server.UI.Services;
using Microsoft.EntityFrameworkCore;
using SSO.Api.Infrastructure.Model;
using SSO.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.Configure<JwtInfo>(builder.Configuration.GetSection("Jwt"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AuthenticationDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration["ConnectionString:AuthenticationDB"]));
builder.Services.AddMyDependencyGroup();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
