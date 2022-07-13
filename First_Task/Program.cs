using First_Task.Interfaces;
using First_Task.Repository;
using First_Task.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IFileUploadService, FileUploadService>();
builder.Services.AddTransient<IFileLineRepository, FileLineRepository>();
builder.Services.AddTransient<IFileRepository, FileRepository>();

builder.Configuration.SetBasePath(System.AppContext.BaseDirectory)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=File}/{action=Index}/{id?}");

app.Run();
