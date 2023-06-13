using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess
{
    public class ProjectDate
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
        public string Place { get; set; }
        public double Salary { get; set; }
        public int VolunteersLimit { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
