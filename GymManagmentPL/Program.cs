using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Repositories.Interfaces;
using GymManagmentDAL.Repositories;
using GymManagmentBLL.Mapping;
using Microsoft.EntityFrameworkCore;
using GymManagmentDAL.Data.DataSeed;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.Services.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GymDbcontext>(
    options => {

        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ISessionREpository,SessionRepository>(); 
builder.Services.AddAutoMapper(m => m.AddProfile(new MemberProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new HealthRecordProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new PlanProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new SessionProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new TrainerProfile()));

var app = builder.Build();

// Seed Data
 using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GymDbcontext>();
    var pendingMigrations = context.Database.GetPendingMigrations();
    if (pendingMigrations?.Any()??false)
    {
        context.Database.Migrate();
    }
    GymDbContextSeeding.SeedData(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}")
    .WithStaticAssets();
//app.MapControllerRoute(
//    name: "trainers",     name use in redirect to route 
//    pattern: "Coach/{action}",
//    defaults: new { controller = "Trainer" }) ;                  
   


app.Run();
