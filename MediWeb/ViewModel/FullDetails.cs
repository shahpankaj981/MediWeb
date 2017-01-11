using MediWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;


namespace MediWeb.ViewModel
{
    public class FullDetails
    {
        public FullDetails()
        {
            patient = new Patient();
            user = new User();
            problem.Clear();
            problemDate.Clear();
        }
        
        //public Patient patient = new Patient() ;
        public Patient patient { get; set; }

        public User user { get; set; }

        public List<Allergies> allergy = new List<Allergies>();
        public List<string> allergyDate = new List<string>();

        public List<Medicine> medicine = new List<Medicine>();
        public List<string> prescriptionDate = new List<string>();
        public List<Doctor> prescribedBy = new List<Doctor>();

        public List<Test> test = new List<Test>();
        public List<string> testDate = new List<string>();

        public List<string> testReadings = new List<string>();

        public List<string> problem = new List<string>();
        public List<string> problemDate = new List<string>();
        //[Display(Name = "Date Of Allergy")]
        //public string dateOfAllergy { get; set; }







        //[Display(Name = "Date Of Prescription")]
        //public string dateOfPrescription { get; set; }

        //public Test test = new Test();


        //[Display(Name = "Test Readings")]
        //public string testReadings { get; set; }

        //[Display(Name = "Date Of Test")]
        //public string dateOfTest { get; set; }
    }
}