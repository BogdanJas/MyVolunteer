using System.ComponentModel.DataAnnotations;

namespace MyVolunteer_Models
{
    public class ProjectSignUpDetailDTO
    {
        public int Id { get; set; }
        [Required]
        public int ProjectSignUpHeaderId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } 
        public ProjectDTO Project { get; set; }


    }
}
