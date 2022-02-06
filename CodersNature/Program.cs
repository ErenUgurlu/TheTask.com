using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CodersNature.Data;
using CodersNature.Models;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CodersNatureContextConnection");builder.Services.AddDbContext<CodersNatureContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<CodersNatureUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<CodersNatureContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
