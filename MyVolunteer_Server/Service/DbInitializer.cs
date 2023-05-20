using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyVolunteer_Common;
using MyVolunteer_DataAccess.Data;
using MyVolunteer_Server.Service.IService;

namespace MyVolunteer_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Organisation)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                IdentityUser userAdmin = new()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                IdentityUser userOrganisation = new()
                {
                    UserName = "organisation@gmail.com",
                    Email = "organisation@gmail.com",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(userAdmin, "NewPassword1!").GetAwaiter().GetResult();
                _userManager.CreateAsync(userOrganisation, "Password1!").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(userAdmin, SD.Role_Admin).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(userOrganisation, SD.Role_Organisation).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
