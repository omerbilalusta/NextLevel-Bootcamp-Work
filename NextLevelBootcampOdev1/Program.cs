using Microsoft.EntityFrameworkCore;
using NextLevelBootcampOdev1.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //Appsetting.json 'da tanimlanmis ilgili string'i aliyor.

//Automapper dependency 'sini servis olarak inject etmeliyiz kullanabilmek için. (Dependency Enjection)
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
// scaffold-dbcontext "Server=(localdb)\MSSQLLocalDB;Database=NORTHWIND;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -Output: Models -Context:NorthwindDbContext
// Data Source=(localdb)\MSSQLLocalDB;Encrypt=False;