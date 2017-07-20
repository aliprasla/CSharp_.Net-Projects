using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Prasla_Ali_HW6.Models
{
    public class Event
    {

        // Unique identifier for primary key. Auto-generated.
        [Display(Name = "EventID")]
        public Int32 EventID { get; set; }

        // Event Title
        [Required(ErrorMessage = "Please specify the title of the event.")]
        [Display(Name = "Event Title")]
        public String Title { get; set; }

        // Event date
        [Required(ErrorMessage = "Please enter the date of the event.")]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public String Date { get; set; }

        // Event Location
        [Required(ErrorMessage = "Please specify the location of the event.")]
        [Display(Name = "Event Location")]
        public String Location { get; set; }

        // Members-only Boolean
        [Required(ErrorMessage = "Please state whether only members may attend.")]
        public bool MembersOnly { get; set; }

        //navigation properties
        [Display(Name = "Sponsoring Committee")]
        public virtual Committee Committee { get; set; }
        public virtual List<AppUser> AppUsers { get; set; }
    }
}