using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_DataAccess
{
    public class ProjectSignUp
    {
        public ProjectSignUpHeader ProjectSignUpHeader { get; set; }  
        public List<ProjectSignUpDetail> ProjectSignUpDetails { get; set; }
    }
}
