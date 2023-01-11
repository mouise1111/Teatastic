using Microsoft.EntityFrameworkCore;
using Teatastic.Areas.Identity.Data;
using Teatastic.Data;
using Microsoft.AspNetCore.Identity;
using Teatastic.Models;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Infrastructure.Internal;
using Microsoft.AspNetCore.Identity.UI.Services;
using Teatastic.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TeatasticContextConnection") ?? throw new InvalidOperationException("Connection string 'TeatasticContextConnection' not found.");

builder.Services.AddDbContext<TeatasticContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TeatasticUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TeatasticContext>();

// Manueel toegevoegd om te werken met Identity Framework
builder.Services.AddDbContext<global::Teatastic.Data.TeatasticContext>((global::Microsoft.EntityFrameworkCore.DbContextOptionsBuilder options) =>
    options.UseSqlServer(connectionString));



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.Configure<MailKitOptions>(options =>
{
    options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
    options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
    options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
    options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
    options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
    options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];

    // Set it to TRUE to enable ssl or tls, FALSE otherwise
    options.Security = false;  // true zet ssl or tls aan
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<TeatasticUser>>();
    await SeedDataContext.Initialize(services, userManager);
}



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
