using MyVolunteer_Client.Pages;
using MyVolunteer_Client.Service;
using MyVolunteer_Models;
using System.ComponentModel.DataAnnotations;

namespace MyVolunteer_Client.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            ProjectDate = new();
        }
        [Required]
        public int SelectedProjectDateId { get; set; }
        public ProjectDateDTO ProjectDate { get; set; }

    }
}
