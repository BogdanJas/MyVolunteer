using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess
{
    public class Volunteer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Sex { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
