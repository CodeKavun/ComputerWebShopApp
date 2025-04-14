using ComputerWebShopApp.Data;
using ComputerWebShopApp.Infrastructure.BinderProviders;
using ComputerWebShopApp.Profiles;
using ComputerWebShopApp.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connStr = builder.Configuration.GetConnectionString("MSSQLShopDb") ??
    throw new InvalidOperationException("You should provide connection string!");

builder.Services.AddDbContext<ShopContext>(options =>
options.UseSqlServer(connStr));
builder.Services.AddControllersWithViews( options => {
    options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
});
//builder.Services.AddAuthorization(options => {
//    options.
//});
builder.Services.AddIdentity<ShopUser, IdentityRole>(
    options => {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
    })
    .AddEntityFrameworkStores<ShopContext>();
builder.Services.AddScoped<IAuthorizationRequirement, MinimalAgeRequirement>(); 
builder.Services.AddScoped<IAuthorizationHandler, MinimalAgeAuthorizationHandler>();
builder.Services.AddAuthentication().AddGoogle(options => {
    IConfigurationSection googleSection = builder.Configuration.GetSection("Authentication:Google");
    string clientId = googleSection.GetValue<string>("ClientId") ?? 
    throw new InvalidOperationException("Please provide ClientId!");
    string clientSecret = googleSection.GetValue<string>("ClientSecret") ??
    throw new InvalidOperationException("Please provide Client Secret!");
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
});
builder.Services.AddAuthorization(configure => {
    configure.AddPolicy("managerPolicy", policyBuilder =>
    {
        policyBuilder.RequireRole("manager");
        policyBuilder.RequireClaim("Hobbie", "Fishing");
    });
    configure.AddPolicy("hasAppropAge", policyBuilder => {
        policyBuilder.RequireRole("manager");
        policyBuilder.Requirements.Add(new MinimalAgeRequirement(minimalAge: 18));
    });
});
builder.Services.AddAutoMapper(typeof(ShopUserProfile), typeof(RoleProfile),
    typeof(BrandProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
