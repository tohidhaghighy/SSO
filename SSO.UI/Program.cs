using Authentication_Server.UI.Middlewares;
using Authentication_Server.UI.Services;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;
using SSO.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuthenticationDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration["ConnectionString:AuthenticationDB"]));
builder.Services.Configure<JwtInfo>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddMyDependencyGroup();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000", "http://127.0.0.1:1889", "http://127.0.0.1:3000")
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}
app.UseMiddleware<InjectionMiddleware>();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
