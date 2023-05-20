using System.ComponentModel.DataAnnotations;

namespace MyVolunteer_Models
{
    public class ProjectDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool OrganisationFavourite { get; set; }
        public bool VolunteerFavourite { get; set; }
        public string ImageUrl { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public ICollection<ProjectDateDTO> ProjectDates { get; set; }
    }
}
