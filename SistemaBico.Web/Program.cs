using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Serilog;
using SistemaBico.Web.AutoMapper;
using SistemaBico.Web.Services;
using SistemaBico.Web.Services.Interfaces;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.File("logWeb.txt", rollingInterval: RollingInterval.Day)
               .CreateLogger();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Login/login/"); //401 - Unauthorized
        options.AccessDeniedPath = new PathString("/Login/login/"); //403 - Forbidden
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

//if (!builder.Environment.IsDevelopment())
//{
//    builder.Services.AddHttpsRedirection(options =>
//    {
//        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
//        options.HttpsPort = 443;
//    });
//}


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IProfessionalClientService, ProfessionalClientService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
//builder.Services.AddLettuceEncrypt();

var app = builder.Build();


app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
            response.StatusCode == (int)HttpStatusCode.Forbidden)
        response.Redirect("/Login/login/");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
