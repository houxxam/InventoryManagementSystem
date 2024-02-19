using InvWebApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Login section
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
	option.LoginPath = "/Admin/Login";
	option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddDbContext<AppDbContext>(options =>
	{
		var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
		options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
	}
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

//Login section
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Admin}/{action=Login}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
// pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();