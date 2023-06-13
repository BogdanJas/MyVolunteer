using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyVolunteer_Business.Repository;
using MyVolunteer_Business.Repository.IRepository;
using MyVolunteer_DataAccess.Data;
using MyVolunteer_Server.Data;
using MyVolunteer_Server.Service.IService;
using MyVolunteer_Server.Service;
using Syncfusion.Blazor;
using AutoMapper;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTg3OTgyMUAzMjMxMmUzMTJlMzMzNW5adUtHYmdnQVVIb2JvU2VZakE0cmdCZk16cGdSTVkvR1g2UFliL1VqRzA9");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectDateRepository, ProjectDateRepository>();
builder.Services.AddScoped<IProjectSignUpRepository, ProjectSignUpRepository>();
builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IFileUpload, FileUpload>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
