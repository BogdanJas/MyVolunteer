using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess
{
    public class ProjectSignUpDetail
    {
        public int Id { get; set; }
        [Required]
        public int ProjectSignUpHeaderId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } 
        [ForeignKey("ProjectId")]
        [NotMapped]
        public virtual Project Project { get; set; }


    }
}
