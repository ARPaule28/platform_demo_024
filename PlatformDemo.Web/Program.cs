using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// Configure EF Core DbContext with LocalDB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PlatformDemoDb;Trusted_Connection=True;"));

// Build the app
var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

// Map Razor Pages
app.MapRazorPages();

// Run the app
app.Run();
