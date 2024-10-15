using Microsoft.EntityFrameworkCore;
using MovieProject.Context;
using MovieProject.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<MovieService>();
builder.Services.AddScoped<MovieService>(); // Ekleniyor


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
    pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
