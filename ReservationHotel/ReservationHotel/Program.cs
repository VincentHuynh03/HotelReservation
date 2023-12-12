using Microsoft.EntityFrameworkCore;
using ReservationHotel.Models;
using ReservationHotel.Models.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add signalR
builder.Services.AddSignalR();

//Add la gestion des sessions
builder.Services.AddDistributedMemoryCache();
// Utilisation de la m�moire pour stocker les sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // D�finir la dur�e de la session
    options.Cookie.Name = ".AspNetCore.Session"; // Nom du cookie de session
});


// Ajoutez la configuration du service IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configuration de la cha�ne de connexion � partir de appsettings.json
var configuration = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("appsettings.json")
 .Build();
// pour une base des donn�es Sql Server
var connectionString = configuration.GetConnectionString("LocalSqlServerConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();




builder.Services.AddRazorPages();



var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Appliquer les migrations si n�cessaire
    dbContext.SeedData(); // Ajouter des donn�es de test
}





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapHub<ChatHub>("/chatHub");
app.MapHub<AskHub>("/askHub");
app.MapRazorPages();
app.Run();
