using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediWeb.Models
{
    public class Medicine
    {
        
        [Display(Name = "Medicine ID")]
        public string MID { get; set; }
        
        [Display(Name = "Medicine Name")]
        public string medicineName { get; set; }
    }
}