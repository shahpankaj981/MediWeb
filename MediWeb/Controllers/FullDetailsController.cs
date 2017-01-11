using MediWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediWeb.Models;
using System.IO;

namespace MediWeb.ViewModel
{
    public class FullDetailsController : Controller
    {
        //
        // GET: /FullDetails/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addPatient()
        {
            patientRepository prp = new patientRepository();
            List<SelectListItem> dbAllergyList = prp.getAllergyItems();
            List<SelectListItem> dbMedicineList = prp.getMedicineItems();
            List<SelectListItem> dbTestList = prp.getTestItems();
            List<SelectListItem> dbDoctorList = prp.getDoctorItems();
            //List<SelectListItem> gender = new List<SelectListItem>(
            //                                {
            //                                new SelectListItem { Text = "Male", Value = "M" },
            //                                new SelectListItem { Text = "Female", Value = "F" }
            //                                }, "Value" , "Text");
            ViewBag.allergies = dbAllergyList;
            ViewBag.medicine = dbMedicineList;
            ViewBag.test = dbTestList;
            ViewBag.doctor = dbDoctorList;

            return View();
        }

        [HttpPost]
        public ActionResult addPatient(FullDetails ptnt)
        {
            patientRepository pr = new patientRepository();
            ptnt.patient.photo = "none.jpg";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                    file.SaveAs(path);

                    ptnt.patient.photo = filename; //getting image
                }

            }

            string allergies = Request["allergies"].ToString();
            //string allergyID = Request["allergyID"].ToString();
            string allergyDate = Request["allergyDate"].ToString();

            System.Diagnostics.Debug.Print("allergies=" + allergies);
            System.Diagnostics.Debug.Print("Name: " + ptnt.patient.name);

            string[] allergiesList = allergies.Split(',');
            System.Diagnostics.Debug.Print("allergy: " + allergiesList[0]);
            //string[] allergyIDList = allergyID.Split(',');

            string[] allergyDateList = allergyDate.Split(',');

            for (int i = 0; i < allergiesList.Length; i++)
            {
                Allergies a = new Allergies();
                a.AID = Convert.ToString(allergiesList[i]);
                //a.allergyType = Convert.ToString(allergiesList[i]);
                if (String.IsNullOrWhiteSpace(a.AID))
                {
                    continue;
                }
                System.Diagnostics.Debug.Print("ID=" + a.AID);
                ptnt.allergy.Add(a);
                ptnt.allergyDate.Add(allergyDateList[i]);
            }

            string problem = Request["problem"].ToString();

            string problemDate = Request["problemDate"].ToString();

            System.Diagnostics.Debug.Print("problems=" + allergies);


            string[] problemList = problem.Split(',');
            System.Diagnostics.Debug.Print("problem: " + problemList[0]);
            //string[] allergyIDList = allergyID.Split(',');

            string[] problemDateList = problemDate.Split(',');

            for (int i = 0; i < problemList.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(problemList[i]))
                {
                    continue;
                }
                System.Diagnostics.Debug.Print("problem=" + problemList[i]);
                ptnt.problem.Add(problemList[i]);
                ptnt.problemDate.Add(problemDateList[i]);
            }

            string medicinename = Request["medicine"].ToString();
            //string medicineID = Request["medicineID"].ToString();
            string prescriptionDate = Request["prescriptionDate"].ToString();
            //string prescriptionByDID = Request["prescribedByDID"].ToString();
            string prescriptionBy = Request["doctor"].ToString();

            string[] medicineList = medicinename.Split(',');
            //string[] medicineIDList = medicineID.Split(',');
            string[] medicineDateList = prescriptionDate.Split(',');
            //string[] prescribedByListDID = prescriptionBy.Split(',');
            string[] doctorList = prescriptionBy.Split(',');

            for (int i = 0; i < medicineList.Length; i++)
            {
                Medicine m = new Medicine();
                m.MID = Convert.ToString(medicineList[i]);
                //m.medicineName = Convert.ToString(medicineList[i]);
                if (String.IsNullOrWhiteSpace(m.MID))
                {
                    continue;
                }
                ptnt.medicine.Add(m);
                ptnt.prescriptionDate.Add(medicineDateList[i]);
                Doctor d = new Doctor();
                d.DID = doctorList[i];
                //d.doctorName = prescribedByList[i];
                ptnt.prescribedBy.Add(d);
            }

            string testname = Request["test"].ToString();
            //string testID = Request["testID"].ToString();
            string testDate = Request["testDate"].ToString();
            string testReadings = Request["testReadings"].ToString();

            string[] testList = testname.Split(',');
            //string[] testIDList = testID.Split(',');
            string[] testDateList = testDate.Split(',');
            string[] testReadingsList = testReadings.Split(',');
            for (int i = 0; i < testList.Length; i++)
            {
                Test t = new Test();
                t.TID = Convert.ToString(testList[i]);
                //t.testName = Convert.ToString(testList[i]);
                if (String.IsNullOrWhiteSpace(t.TID))
                {
                    continue;
                }
                ptnt.test.Add(t);
                ptnt.testDate.Add(testDateList[i]);
                ptnt.testReadings.Add(testReadingsList[i]);
            }


            pr.addPatient(ptnt);
            return View("patientAdded", ptnt);
        }

        //public ActionResult viewSearch()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult viewSearch(string pid)
        {

            string str = pid;
            //Request["ptntSearch"].ToString();
            System.Diagnostics.Debug.Print("" + str);
            patientRepository pr = new patientRepository();
            FullDetails p = pr.viewSearch(str);

            return View("viewSearchDetails", p);
        }

        //[HttpPost]
        //public ActionResult viewSearch(string a)
        //{

        //    string str = Request["ptntSearch"].ToString();
        //    System.Diagnostics.Debug.Print("" + str);
        //    patientRepository pr = new patientRepository();
        //    FullDetails p = pr.viewSearch(str);

        //    return View("viewSearchDetails", p);
        //}

        [Authorize]
        public ActionResult edit(string id)
        {
            patientRepository prp = new patientRepository();
            FullDetails p = prp.viewSearch(id);
            List<SelectListItem> dbAllergyList = prp.getAllergyItems();
            List<SelectListItem> dbMedicineList = prp.getMedicineItems();
            List<SelectListItem> dbTestList = prp.getTestItems();
            List<SelectListItem> dbDoctorList = prp.getDoctorItems();
            ViewBag.allergies = dbAllergyList;
            ViewBag.medicine = dbMedicineList;
            ViewBag.test = dbTestList;
            ViewBag.doctor = dbDoctorList;


            return View(p);
        }

        [Authorize]
        [HttpPost]
        public ActionResult edit(FullDetails ptnt)
        {
            System.Diagnostics.Debug.Print("" + ptnt.patient.name);
            string allergies = Request["allergies"].ToString();
            //string allergyID = Request["allergyID"].ToString();
            string allergyDate = Request["allergyDate"].ToString();

            System.Diagnostics.Debug.Print("allergies=" + allergies);
            System.Diagnostics.Debug.Print("Name: " + ptnt.patient.name);

            string[] allergiesList = allergies.Split(',');
            System.Diagnostics.Debug.Print("allergy: " + allergiesList[0]);
            //string[] allergyIDList = allergyID.Split(',');

            string[] allergyDateList = allergyDate.Split(',');

            for (int i = 0; i < allergiesList.Length; i++)
            {
                Allergies a = new Allergies();
                a.AID = Convert.ToString(allergiesList[i]);
                //a.allergyType = Convert.ToString(allergiesList[i]);
                if (String.IsNullOrWhiteSpace(a.AID))
                {
                    continue;
                }
                System.Diagnostics.Debug.Print("ID=" + a.AID);
                ptnt.allergy.Add(a);
                ptnt.allergyDate.Add(allergyDateList[i]);
            }

            string problem = Request["problem"].ToString();

            string problemDate = Request["problemDate"].ToString();

            System.Diagnostics.Debug.Print("problems=" + allergies);


            string[] problemList = problem.Split(',');
            System.Diagnostics.Debug.Print("problem: " + problemList[0]);
            //string[] allergyIDList = allergyID.Split(',');

            string[] problemDateList = problemDate.Split(',');

            for (int i = 0; i < problemList.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(problemList[i]))
                {
                    continue;
                }
                System.Diagnostics.Debug.Print("problem=" + problemList[i]);
                ptnt.problem.Add(problemList[i]);
                ptnt.problemDate.Add(problemDateList[i]);
            }

            string medicinename = Request["medicine"].ToString();
            //string medicineID = Request["medicineID"].ToString();
            string prescriptionDate = Request["prescriptionDate"].ToString();
            //string prescriptionByDID = Request["prescribedByDID"].ToString();
            string prescriptionBy = Request["doctor"].ToString();

            string[] medicineList = medicinename.Split(',');
            //string[] medicineIDList = medicineID.Split(',');
            string[] medicineDateList = prescriptionDate.Split(',');
            //string[] prescribedByListDID = prescriptionBy.Split(',');
            string[] doctorList = prescriptionBy.Split(',');

            for (int i = 0; i < medicineList.Length; i++)
            {
                Medicine m = new Medicine();
                m.MID = Convert.ToString(medicineList[i]);
                //m.medicineName = Convert.ToString(medicineList[i]);
                if (String.IsNullOrWhiteSpace(m.MID))
                {
                    continue;
                }
                ptnt.medicine.Add(m);
                ptnt.prescriptionDate.Add(medicineDateList[i]);
                Doctor d = new Doctor();
                d.DID = doctorList[i];
                //d.doctorName = prescribedByList[i];
                ptnt.prescribedBy.Add(d);
            }

            string testname = Request["test"].ToString();
            //string testID = Request["testID"].ToString();
            string testDate = Request["testDate"].ToString();
            string testReadings = Request["testReadings"].ToString();

            string[] testList = testname.Split(',');
            //string[] testIDList = testID.Split(',');
            string[] testDateList = testDate.Split(',');
            string[] testReadingsList = testReadings.Split(',');
            for (int i = 0; i < testList.Length; i++)
            {
                Test t = new Test();
                t.TID = Convert.ToString(testList[i]);
                //t.testName = Convert.ToString(testList[i]);
                if (String.IsNullOrWhiteSpace(t.TID))
                {
                    continue;
                }
                ptnt.test.Add(t);
                ptnt.testDate.Add(testDateList[i]);
                ptnt.testReadings.Add(testReadingsList[i]);
            }

            patientRepository pr = new patientRepository();
            pr.editPatient(ptnt);
            return View("patientEdited", ptnt);
        }

    }
}