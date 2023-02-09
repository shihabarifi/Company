using Company.Data;
using Company.IRepository;
using Company.Models;
using Company.Repositories;
using Company.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSession();
builder.Services.AddScoped<IRepository<Service>, ServiceRepo>();
builder.Services.AddScoped<IRepository<About>, AboutRepo>();
builder.Services.AddDbContext<CompanyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<CompanyDbContext>();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin";
    options.AccessDeniedPath = "/Admin/Home/Denied";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
