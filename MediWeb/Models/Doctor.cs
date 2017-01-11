using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediWeb.Models
{
    public class Doctor
    {
        
        [Display(Name = "Doctor ID")]
        public string DID { get; set; }
        
        [Display(Name = "Doctor Name")]
        public string doctorName { get; set; }
    }
}