using AdvDemo;
using AdvDemo.Exceptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// register logging middleware
builder.Services.AddHttpLogging(o => { });
builder.Logging.AddFilter(
    "Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

// Set up database configuration.

string? connStringName = builder.Configuration["DbConnectionStringName"] ??
    throw new MissingConfigurationException("DB Connection string name is not defined.  Make sure to set DbConnectionStringName in your configuration.");

string? connString = builder.Configuration[$"ConnectionStrings:{connStringName}"] ??
    throw new MissingConfigurationException($"DB connection string is not defined.  Make sure to set your connection string named {connStringName} in your configuration.");

builder.Services.AddDbContext<AdventureWorksContext>(options => 
    options.UseSqlServer(connString));

// Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/appConfig", (IConfiguration config) => config.AsEnumerable());
app.MapGet("/appEnv", () => Environment.GetEnvironmentVariables());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
