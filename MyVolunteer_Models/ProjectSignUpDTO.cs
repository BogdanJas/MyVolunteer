namespace MyVolunteer_Models
{
    public class ProjectSignUpDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ProjectDateId { get; set; }
        public int VolunteerId { get; set; }
        public DateTime SignDate { get; set; }
        //public virtual ProjectDTO Project { get; set; }
        //public virtual ProjectDateDTO ProjectDate { get; set; }
        //public virtual VolunteerDTO Volunteer { get; set; }
    }
}
