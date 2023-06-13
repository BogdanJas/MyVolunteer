 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }    
        public DbSet<ProjectDate> ProjectDates { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<ProjectSignUp> ProjectSignUps { get; set; }
    }
}
