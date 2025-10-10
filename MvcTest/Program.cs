using Microsoft.EntityFrameworkCore;
using Mvc.BLL.Interfaces;
using Mvc.BLL.Repositories;
using Mvc.DAL.Contexts;
using Mvc.PAL.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MvcDbcontext>(Options =>
 {
     Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
 });

builder.Services.AddScoped<IDepartmentRepo,DepartmentRepo>();
builder.Services.AddScoped<IEmployeeRepo,EmployeeRepo>();
builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
