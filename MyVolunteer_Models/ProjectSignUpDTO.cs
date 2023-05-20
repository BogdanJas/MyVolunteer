namespace MyVolunteer_Models
{
    public class ProjectSignUpDTO
    {
        public ProjectSignUpHeaderDTO ProjectSignUpHeader { get; set; }  
        public List<ProjectSignUpDetailDTO> ProjectSignUpDetail { get; set; }
    }
}
