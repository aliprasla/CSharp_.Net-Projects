using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Prasla_Ali_HW6.Models
{
    public class Committee
    {
        public int CommitteeID { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}