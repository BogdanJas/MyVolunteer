using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess
{
    public class ProjectSignUp
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public int? ProjectDateId { get; set; }
        [ForeignKey("ProjectDateId")]
        public ProjectDate ProjectDate { get; set; }
        public int? VolunteerId { get; set; }
        [ForeignKey("VolunteerId")]
        public Volunteer Volunteer { get; set; }
        public DateTime SignDate { get; set; }  
    }
}
