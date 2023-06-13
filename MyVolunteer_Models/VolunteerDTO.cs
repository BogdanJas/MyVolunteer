using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_Models
{
    public class VolunteerDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Sex { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password is not matched")]
        public string? ConfirmPassword { get; set; }
        public string? SignInId { get; set; }
    }
}
