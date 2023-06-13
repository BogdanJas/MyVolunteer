namespace MyVolunteer_Models
{
    public class ProjectSignUpDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string? ProjectPlace { get; set; }
        public int VolunteerId { get; set; }
        public string? VolunteerName { get; set; }
        public string? VolunteerEmail { get; set; }
        public string? VolunteerPhoneNumber { get; set; }
        public DateTime? VolunteerDateOfBirth { get; set; }
        public bool? VolunteerSex { get; set; }
        public DateTime SignDate { get; set; }
    }
}
