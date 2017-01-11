using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediWeb.Models
{
    public class Test
    {
        
        [Display(Name = "Test ID")]
        public string TID { get; set; }
        [Display(Name = "Test Name")]
        public string testName { get; set; }
    }
}