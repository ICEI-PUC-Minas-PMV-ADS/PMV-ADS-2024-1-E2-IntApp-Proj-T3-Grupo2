using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Padrinly.Data;
using Padrinly.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

var IsDevelopment = Environment
                    .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var connectionString = IsDevelopment ?
      builder.Configuration.GetConnectionString("DefaultConnection") :
      GetHerokuConnectionString();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Executa as migrações automaticamente (se desejado, especialmente útil para o deploy no Heroku)
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/apply-migrations")
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
            await context.Response.WriteAsync("Migrations applied successfully.");
            return;
        }
    }
    await next();
});

app.InitializeDatabase(app.Environment.IsDevelopment());
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

static string ConvertDatabaseUrlToConnectionString(string databaseUrl)
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    return $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=True;";
}

static string ConvertHerokuUrlToEntityFramework(string databaseUrl)
{
    if (string.IsNullOrEmpty(databaseUrl))
        return null;

    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};MultipleActiveResultSets=true;Trusted_Connection=True;";

    return connectionString;
}

static string GetHerokuConnectionString()
{
    string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    var databaseUri = new Uri(connectionUrl);

    string db = databaseUri.LocalPath.TrimStart('/');

    string[] userInfo = databaseUri.UserInfo
                        .Split(':', StringSplitOptions.RemoveEmptyEntries);

    return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};" +
           $"Port={databaseUri.Port};Database={db};Pooling=true;" +
           $"SSL Mode=Require;Trust Server Certificate=True;";
}
