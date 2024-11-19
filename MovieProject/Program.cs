using Microsoft.EntityFrameworkCore;
using MovieProject.Context;
using MovieProject.Services;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);
/*MsSql için konfigürasyon ayarlarý
*/
//builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnectionString")));

/*PostgreSql için konfigürasyon ayarlarý*/
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));

/*kullanýcý database ismini girmeden direk otomatik olarak baðlanacak þekilde oluþturulmuþ olan konfigürasyon ayarlarý !!olmadý yetkilendirme ile ilgili problem var !!*/
//var serverName = Environment.MachineName; // Kullanýcý bilgisayar adýný alýr
//var connectionString = $"Data Source={serverName}\\SQLEXPRESS;Initial Catalog=FilmApplication;Integrated Security=True;TrustServerCertificate=True;";
//builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<MovieService>();
builder.Services.AddScoped<MovieService>(); // Ekleniyor

var app = builder.Build();
/*Otomatikmen migration iþlemi yapan kod bloðu */
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MovieContext>();
    dbContext.Database.Migrate();
}
/*Otomatikmen migration iþlemi yapan kod bloðu */

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
