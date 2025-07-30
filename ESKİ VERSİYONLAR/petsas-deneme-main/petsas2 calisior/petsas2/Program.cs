using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using petsas2.Components;
using petsas2.Components.Account;
using petsas2.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using petsas2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//mudblazor için
builder.Services.AddMudServices();

// Ürün servisi
builder.Services.AddScoped<IProductService, ProductService>();
//
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); //detaylı hata goster

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

////

var app = builder.Build();

//Seed burada sadece rolleri ve başlangıç Admin/Supplier hesaplarını oluşturur.
//seed baslangıç
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedIdentityDataAsync(services);
}
static async Task SeedIdentityDataAsync(IServiceProvider services)
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    //Roller admin-tedarikci-kullanıcı
    string[] roles = new[] { "Admin", "Supplier", "User" };
    foreach (var roleName in roles)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Admin
    var adminData = new[]
    {
        new { Email = "admin1@petsas.com", Password = "Admin1!" },
        new { Email = "admin2@petsas.com", Password = "Admin2!" }
    };

    foreach (var adm in adminData)
    {
        if (await userManager.FindByEmailAsync(adm.Email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = adm.Email,
                Email = adm.Email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, adm.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {

            }
        }
    }

    //  Supplier 
    var supplierEmail = "supplier@petsas.com";
    var supplierPwd = "Supplier1!";
    if (await userManager.FindByEmailAsync(supplierEmail) == null)
    {
        var supplier = new ApplicationUser
        {
            UserName = supplierEmail,
            Email = supplierEmail,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(supplier, supplierPwd);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(supplier, "Supplier");
        }
    }
    //user
    var userEmail = "user@petsas.com";
    var userPwd = "User1!";
    if (await userManager.FindByEmailAsync(userEmail) == null)
    {
        var normalUser = new ApplicationUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(normalUser, userPwd);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(normalUser, "User");
        }
    }
}
//seed bitis

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//bu sıralanıs onemli
app.UseHttpsRedirection();
app.UseStaticFiles(); //wwwroot için
app.UseAntiforgery(); //koruma
app.MapRazorComponents<App>() //componentler için
    .AddInteractiveServerRenderMode();
app.MapAdditionalIdentityEndpoints(); //oto kaydet
app.Run(); //calıs!!!!
