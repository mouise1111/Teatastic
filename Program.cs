using Microsoft.EntityFrameworkCore;
using Teatastic.Areas.Identity.Data;
using Teatastic.Data;
using Microsoft.AspNetCore.Identity;
using Teatastic.Models;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TeatasticContextConnection") ?? throw new InvalidOperationException("Connection string 'TeatasticContextConnection' not found.");

builder.Services.AddDbContext<TeatasticContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TeatasticUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TeatasticContext>();

/*builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();*/

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//var host = CreateHostBuilder(args).Build();

//    using (var scope = host.Services.CreateScope())
//    {
//        var services = scope.ServiceProvider;
//        try
//        {
//            SeedData.Initialize(services);
//        }
//        catch (Exception ex)
//        {
//            var logger = services.GetRequiredService<ILogger<Program>>();
//            logger.LogError(ex, "An error occurred seeding the DB.");
//        }
//    }

//    host.Run();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
