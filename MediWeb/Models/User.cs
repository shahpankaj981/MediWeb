using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediWeb.Models
{
    public class User
    {
        
        [Required]
        public string id { get; set; }

        //public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}