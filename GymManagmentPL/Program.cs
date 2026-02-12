using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Repositories.Interfaces;
using GymManagmentDAL.Repositories;
using GymManagmentBLL.Mapping;
using Microsoft.EntityFrameworkCore;
using GymManagmentDAL.Data.DataSeed;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.AttachementService;
using Microsoft.AspNetCore.Identity;
using GymManagmentDAL.Entites;

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
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMemberShipService, MemberShipService>();
builder.Services.AddScoped<ISessionREpository,SessionRepository>(); 
builder.Services.AddScoped<IMemberShipRepository,MemberShipRepository>(); 
builder.Services.AddScoped<IMemberSessionRepository,MemberSessionRepository>(); 
builder.Services.AddScoped<IMemberSesionService,MemberSessionService>(); 
builder.Services.AddAutoMapper(m => m.AddProfile(new MemberProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new HealthRecordProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new PlanProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new SessionProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new TrainerProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new MemberShipProfile()));
builder.Services.AddAutoMapper(m => m.AddProfile(new MemberSessionProfile()));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>
    (
     options =>{

        options.User.RequireUniqueEmail = true;

    }).AddEntityFrameworkStores<GymDbcontext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
   
});
var app = builder.Build();

// Seed Data
 using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GymDbcontext>();
    var pendingMigrations = context.Database.GetPendingMigrations();
    if (pendingMigrations?.Any()??false)
    {
        context.Database.Migrate();
    }
    GymDbContextSeeding.SeedData(context);
    await IdentitySeeding.SeedDataAsync(roleManager, userManager);
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
app.UseAuthentication();    
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id:int?}")
    .WithStaticAssets();
//app.MapControllerRoute(
//    name: "trainers",     name use in redirect to route 
//    pattern: "Coach/{action}",
//    defaults: new { controller = "Trainer" }) ;                  
   


app.Run();
