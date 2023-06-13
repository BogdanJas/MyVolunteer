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
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        public ProjectDate ProjectDate { get; set; }
        [ForeignKey("Id")]
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public DateTime SignDate { get; set; }
    }
}
