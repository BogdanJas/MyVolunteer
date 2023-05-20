using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess
{
    public class ProjectSignUpHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VolunteerId { get; set; }
        public virtual Volunteer Volunteer { get; set;}
        public DateTime SignDate { get; set; }

    }
}
