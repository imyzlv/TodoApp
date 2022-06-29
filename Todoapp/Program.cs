using Todoapp.Database;
using Todoapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//@Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("TodoDbContextConnection") ?? throw new InvalidOperationException("Connection string 'TodoDbContextConnection' not found.");

//builder.Services.AddDbContext<TodoDbContext>(options =>
//options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<TodoDbContext>();;
// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;
    options.Lockout.AllowedForNewUsers = false;
});

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "3185878064996702";
    options.AppSecret = "1dbf3a353cdf324f0bc0537bea8dd30c";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

// Read database config from appsettings.json
// Uncomment for InMemory Db usage:
//builder.Services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DevelopmentConnection")));
// Uncomment for SQLite Db usage:
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedDataTaskList.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();
app.MapRazorPages(); // Needed for identity functionality
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

