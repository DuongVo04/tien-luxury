using Microsoft.EntityFrameworkCore;
using MinhTienHairSalon.Models;
using MinhTienHairSalon.Areas.Admin.Services;
using MinhTienHairSalon.Services;
using MinhTienHairSalon.Data;
using HairSalonWeb.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Thiết lập CultureInfo mặc định cho toàn bộ ứng dụng
var cultureInfo = new CultureInfo("vi-VN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache(); // Cần thiết để sử dụng Session
// Thêm dịch vụ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true; // Bảo mật session
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();


//Lower case url
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Config database
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddDbContext<DBContext>(options =>
options.UseMongoDB(mongoDBSettings.AtlasURI ?? "", mongoDBSettings.DatabaseName ?? ""));

builder.Services.AddScoped<IAdminAccountService, AdminAccountService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationDetailService, ReservationDetailService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceDetailsService, InvoiceDetailsService>();
builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo)
});

app.UseSession(); // Kích hoạt Session
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=ProductDetail}/{id?}");

app.Run();
