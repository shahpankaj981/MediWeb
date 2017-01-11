using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediWeb.Models
{
    public class Allergies
    {
        
        [Display(Name = "Allergy ID")]
        public string AID { get; set; }
        
        [Display(Name = "Allergy Type")]
        public string allergyType { get; set; }
    }
}