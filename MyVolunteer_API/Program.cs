using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyVolunteer_API.Helper;
using MyVolunteer_Business.Repository;
using MyVolunteer_Business.Repository.IRepository;
using MyVolunteer_Client.Service.IService;
using MyVolunteer_DataAccess.Data;
using System.Text;

namespace MyVolunteer_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectDateRepository, ProjectDateRepository>();
            builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            builder.Services.AddScoped<IProjectSignUpRepository, ProjectSignUpRepository>();
            builder.Services.AddScoped<IEmailSender,EmailSender>();
            builder.Services.AddCors(o => o.AddPolicy("MyVolunteer", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

            var apiSettingsSection = builder.Configuration.GetSection("APISettings");
            builder.Services.Configure<APISettings>(apiSettingsSection);

            var apiSettings = apiSettingsSection.Get<APISettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = apiSettings.ValidAudience,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyVolunteer");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}