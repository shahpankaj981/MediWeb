using MediWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using MediWeb.ViewModel;

namespace MediWeb.Controllers
{
    public class PatientController : Controller
    {
        
        // GET: /patient/
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult viewSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult viewSearch(string a)
        {

            string str = Request["ptntSearch"].ToString();
            System.Diagnostics.Debug.Print("" + str);
            patientRepository pr = new patientRepository();
            FullDetails p = pr.viewSearch(str);
            
            return View("viewSearchDetails" , p);
        }

        public ActionResult deletePatient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult deletePatient(string deleteRecord)
        {
            if (deleteRecord != "delete")
            {
                string str = Request["dltPatient"].ToString();
                System.Diagnostics.Debug.Print("" + str);
                patientRepository pr = new patientRepository();
                FullDetails p = pr.deletePatientAsk(str);
                //System.Diagnostics.Debug.Print("" + p.name);
                return View("patientDeleted", p);
            }
            else
                System.Diagnostics.Debug.Print("DONE");
            return View();

            //int dltFlag = deletePatient(p);

            
        }

        
        /*public ActionResult deletePatient(patient p)
        {

            string str = Request["dltPatient"].ToString();
            patientRepository pr = new patientRepository();
            int dltFlag = pr.deletePatient(p);
            if (dltFlag == 1)
                return View("deleteSuccess");
            else
                return HttpNotFound();

          

        }*/

        /*[HttpPost]
        public ActionResult Index(string a)
        {

            string str = Request["ptntSearch"].ToString();
            System.Diagnostics.Debug.Print("" + str);
            return View();
        }
          */

        
        public ActionResult viewDetails()
        {
            patientRepository pr = new patientRepository();
            List<Patient> pl = pr.retrieveData();
           
            return View(pl);
        }

        
        
        // GET: /patient/Create

       /* public ActionResult Create()
        {
            return View("addPatient");
        }
        */


        [HttpPost]
        public ActionResult addPatient(FullDetails ptnt)
        {
            /*string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = cs;

            System.Diagnostics.Debug.Print("" + p.PID);
            System.Diagnostics.Debug.Print("" + p.name);
            string cm = "INSERT into patient Values ('"+ p.PID + "','" + p.name + "','" +  p.DOB + "','" + p.gender  + "','" + p.address + "','" + p.phone + "','" + p.marritalStatus + "','" + p.bloodGroup + "','" + p.fatherName + "','" + p.occupation + "','" + p.height + "','" + p.weight + "')";
            connection.Open();
            SqlCommand cmd = new SqlCommand(cm, connection);
            cmd.ExecuteNonQuery();
            connection.Close(); */
            
            patientRepository pr = new patientRepository();
            pr.addPatient(ptnt);
            
            return View();
        }

        public ActionResult addpatient( )
        {
            return View();
        }
        //
        // POST: /patient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        


        //
        // GET: /patient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
