using TaskManagement.Persistance;
using TaskManagement.Application.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices(); // Application katmanýndaki servisleri ekler

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt => 
{ opt.Cookie.Name = "TaskManagementCookie"; // Cookie adýný belirler
	opt.Cookie.HttpOnly = true; // Cookie'nin HttpOnly özelliðini etkinleþtirir.JavaScript tarafýndan eriþilemez
	opt.Cookie.SameSite= SameSiteMode.Strict; // Cookie'nin SameSite özelliðini ayarlar. Bu, CSRF saldýrýlarýna karþý koruma saðlar. ve sadece ygulamanýn kendi domaininde kullanýlmasýna izin verir
	//opt.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookie'nin güvenliðini saðlar. HTTPS üzerinden gönderilmesini zorunlu kýlar
	opt.Cookie.SecurePolicy=CookieSecurePolicy.SameAsRequest; // Cookie'nin güvenliðini saðlar. HTTP ile geldiyse Http https ile geldiyse https üzerinden gönderilmesini zorunlu kýlar
	//opt.LoginPath = "/Account/Login"; // Giriþ yapma yolu
	//opt.AccessDeniedPath = "/Account/AccessDenied"; // Eriþim reddedildiðinde yönlendirilecek yol
	//opt.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie'nin geçerlilik süresi
	//opt.SlidingExpiration = true; // Cookie'nin süresi her istekle uzatýlýr
});// Cookie tabanlý kimlik doðrulama ekler
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klasöründeki statik dosyalarý kullanabilmek için

app.UseRouting();
app.UseAuthentication(); // Kimlik doðrulama middleware'ini kullanýr
app.UseAuthorization(); // Yetkilendirme middleware'ini kullanýr

app.MapControllerRoute(
	name: "area",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");//Area desteði için bu yapý kullanýlýr. Örneðin, "Admin" adýnda bir alanýnýz varsa, "Admin/Home/Index" gibi URL'ler kullanýlabilir.özel Route alanlarý genelden daha  önde yazýlýr
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}");//Deðiþtirmek için bu yapý kullanýlabilir.

app.Run();
