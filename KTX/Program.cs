using Microsoft.EntityFrameworkCore;
using KTX.Models;
using Wkhtmltopdf.NetCore;
//using KTX.Service;
//using KTX.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KtxDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KtxContext")));
//builder.Services.AddIdentity<KtxDbContext>()
//                .AddEntityFrameworkStores<KtxDbContext>().AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddWkhtmltopdf("wkhtmltopdf");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
