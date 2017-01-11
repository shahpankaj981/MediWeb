using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediWeb.Models
{
    public class Patient
    {
        [HiddenInput]
        public string PID { get; set; }
        //[Required(ErrorMessage = "You should enter Name")]
        [Display(Name = "NAME")]
        public string name { get; set; }
        //[Required(ErrorMessage = "You should enter Address")]
        [Display(Name = "ADDRESS")]
        public string address { get; set; }
        [Display(Name = "DOB")]
        public string DOB { get; set; }
        [Display(Name = "PHONE")]
        public string phone { get; set; }
        [Display(Name = "BLOOD GROUP")]
        public string bloodGroup { get; set; }
        //[Required(ErrorMessage = "You should enter gender")]
        [Display(Name = "GENDER")]
        public string gender { get; set; }
        [Display(Name = "MARRITAL STATUS")]
        public string marritalStatus { get; set; }
        [Display(Name = "FATHER'S NAME")]
        public string fatherName { get; set; }
        [Display(Name = "OCCUPATION")]
        public string occupation { get; set; }
        [Display(Name = "HEIGHT")]
        public string height { get; set; }
        [Display(Name = "WEIGHT")]
        public double weight { get; set; }

        public string photo { get; set; }

    }
}