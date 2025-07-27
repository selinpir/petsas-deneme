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
using petsas2.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//YENİ-Blazor Server uyguulamasında hata ayıklama sürecini kolaylaştırmak için kullanılır
builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
});
//YENİ 

//MUDBLAZOR ICIN
builder.Services.AddMudServices();













builder.Services.AddScoped<ISiparisService, SiparisService>();



//CRUD -SERVIS KAYITLARI
//KULLANICI
//hesap: listele-tek seferlik ekle
builder.Services.AddScoped<IHesapService, HesapService>();
//pet: listele-ekle-sil
builder.Services.AddScoped<IPetService, PetService>();
//adres: listele-tek seferlik ekle
builder.Services.AddScoped<IAdresService, AdresService>();
//sepet islemleri
builder.Services.AddScoped<ISepetService, SepetService>();
//KULLANICI

//ADMİN
//adminde kullanıcıları listelemek icin
builder.Services.AddScoped<IAdminService, AdminService>();
//brand marka servis
builder.Services.AddScoped<IBrandService, BrandService>();
//category kategori servis
builder.Services.AddScoped<ICategoryService, CategoryService>();
//subcategory servisi
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
//Product serivisi
builder.Services.AddScoped<IProductService, ProductService>();
//fiyatlandırma pricing servisi
builder.Services.AddScoped<IPricingService, PricingService>();
//ADMİN
//-------
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
//-------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//-------
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); //detaylı hata goster
//-------
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//-------
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
//-------
//Tüm servislerin ve middleware'lerin derlendiği, uygulama pipeline'ının oluşturulduğu noktadır.
var app = builder.Build();
//KULLANICI ROLLERİ
//Seed burada sadece rolleri ve başlangıç Admin/Supplier hesaplarını oluşturur.
//seed baslangıç
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedIdentityDataAsync(services); //geliştirici tarafından tanımlanan veritabanına başlangıç verileri ekleyen bir metottur
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
    
}
//seed bitis
//KULLANICI ROLLERİ

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
