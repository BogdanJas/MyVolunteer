using System.ComponentModel.DataAnnotations;

namespace MyVolunteer_Models
{
    public class ProjectSignUpHeaderDTO
    {
        public int Id { get; set; }
        [Required]
        public int VolunteerId { get; set; }
        public VolunteerDTO Volunteer { get; set;}
        public DateTime SignDate { get; set; }
        
    }
}
