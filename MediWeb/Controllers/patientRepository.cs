using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MediWeb.Models;
using MediWeb.ViewModel;
using System.Web.Mvc;

namespace MediWeb.Controllers
{
    public class patientRepository
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public SqlConnection connection = new SqlConnection();

        public patientRepository()
        {
            connection.ConnectionString = cs;
        }

        public List<Patient> retrieveData()
        {
            List<Patient> patientList = new List<Patient>();

            SqlCommand cmd = new SqlCommand("select * from patient", connection);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connection.Open();
            da.Fill(dt);
            connection.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Patient p = new Patient();
                p.PID = Convert.ToString(dt.Rows[i]["PID"]);
                p.name = Convert.ToString(dt.Rows[i]["Name"]);
                p.DOB = Convert.ToString(dt.Rows[i]["DOB"]);
                p.gender = Convert.ToString(dt.Rows[i]["Gender"]);
                p.address = Convert.ToString(dt.Rows[i]["Address"]);
                p.phone = Convert.ToString(dt.Rows[i]["Phone"]);
                p.marritalStatus = Convert.ToString(dt.Rows[i]["MarritalStatus"]);
                p.bloodGroup = Convert.ToString(dt.Rows[i]["BloodGroup"]);
                p.fatherName = Convert.ToString(dt.Rows[i]["FatherName"]);
                p.occupation = Convert.ToString(dt.Rows[i]["Occupation"]);
                p.height = Convert.ToString(dt.Rows[i]["Height"]);
                p.weight = Convert.ToDouble(dt.Rows[i]["Weight"]);
                patientList.Add(p);

                // System.Diagnostics.Debug.Print("" + p.PID);            
            }
            //System.Diagnostics.Debug.Print("" + patientList[0].PID);
            //System.Diagnostics.Debug.Print("" + patientList[1].PID);
            //System.Diagnostics.Debug.Print(""+dt.Rows.Count);
            return patientList;
        }

        public FullDetails viewSearch(string search)
        {
            FullDetails p = find(search);
            return p;
        }

        public User getUser(User check)
        {
            SqlCommand cmd = new SqlCommand("select * from UserData where ID='" + check.id + "' and Password = '" + check.password + "'", connection);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            User u = new User();
            u.id = Convert.ToString(dt.Rows[0]["ID"]);
            u.password = Convert.ToString(dt.Rows[0]["Password"]);
            System.Diagnostics.Debug.Print("check=" + check.id + check.password);
            System.Diagnostics.Debug.Print("get=" + u.id + u.password);
            connection.Close();
            return u;
        }

        public string getNextID()
        {
            SqlCommand cmd = new SqlCommand("select top 1 PID from Patient order by PID desc", connection);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string currentID = Convert.ToString(dt.Rows[0]["PID"]);
            System.Diagnostics.Debug.Print("current ID=" + currentID);
            string[] str = currentID.Split('P');
            System.Diagnostics.Debug.Print("strings =" + str[1]);

            string nextID = 'P' + Convert.ToString(Convert.ToInt32(str[1]) + 1);
            System.Diagnostics.Debug.Print("current ID=" + nextID);
            connection.Close();

            return nextID;
        }


        public void addPatient(FullDetails p)
        {
            System.Diagnostics.Debug.Print("id=" + p.patient.PID);
            System.Diagnostics.Debug.Print("name=" + p.patient.name);
            System.Diagnostics.Debug.Print("length=" + p.allergy.Count);
            System.Diagnostics.Debug.Print("password=" + p.user.password);

            System.Diagnostics.Debug.Print("problem=" + p.problem[0]);

            p.patient.PID = getNextID();
            p.user.id = p.patient.PID;

            System.Diagnostics.Debug.Print("password=" + p.user.id);
            System.Diagnostics.Debug.Print("password=" + p.user.password);
            System.Diagnostics.Debug.Print("patient added" + p.patient.marritalStatus);
            //SqlCommand cmd1 = new SqlCommand("INSERT into Patient Values ('" + p.patient.PID + "','" + p.patient.name + "','" + p.patient.DOB + "','" + p.patient.gender + "','" + p.patient.address + "','" + p.patient.phone + "','" + p.patient.marritalStatus + "','" + p.patient.bloodGroup + "','" + p.patient.fatherName + "','" + p.patient.occupation + "','" + p.patient.height + "','" + p.patient.weight + "')", connection);
            SqlCommand cmd1 = new SqlCommand("addNewPatient", connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@PID", p.patient.PID);
            cmd1.Parameters.AddWithValue("@Name", p.patient.name);
            cmd1.Parameters.AddWithValue("@DOB", p.patient.DOB);
            cmd1.Parameters.AddWithValue("@Gender", p.patient.gender);
            cmd1.Parameters.AddWithValue("@Address", p.patient.address);
            cmd1.Parameters.AddWithValue("@Phone", p.patient.phone);
            cmd1.Parameters.AddWithValue("@MarritalStatus", p.patient.marritalStatus);
            cmd1.Parameters.AddWithValue("@BloodGroup", p.patient.bloodGroup);
            cmd1.Parameters.AddWithValue("@FatherName", p.patient.fatherName);
            cmd1.Parameters.AddWithValue("@Occupation", p.patient.occupation);
            cmd1.Parameters.AddWithValue("@Height", p.patient.height);
            cmd1.Parameters.AddWithValue("@Weight", p.patient.weight);
            cmd1.Parameters.AddWithValue("@Photo", p.patient.photo);
            connection.Open();
            int z = cmd1.ExecuteNonQuery();
            System.Diagnostics.Debug.Print("patient added" + p.patient.name);

            if (p.allergy.Count !=0 )
            {
                for (int i = 0; i < p.allergy.Count; i++ )
                {
                    System.Diagnostics.Debug.Print("allergydate=" + p.allergyDate[i]);
                    if (String.IsNullOrWhiteSpace(p.allergy[i].AID))
                    {
                        continue;
                    }
                    SqlCommand cmd2 = new SqlCommand("INSERT into PatientAllergy Values ('" + p.allergyDate[i] + "','" + p.patient.PID + "','" + p.allergy[i].AID + "')", connection);
                    cmd2.ExecuteNonQuery();
                }
            }

            if (p.problem.Count != 0)
            {
                for (int i = 0; i < p.problem.Count; i++)
                {
                    if (String.IsNullOrWhiteSpace(p.problem[i]))
                    {
                        continue;
                    }
                    SqlCommand cmd5 = new SqlCommand("INSERT into Problems Values ('"+ p.patient.PID + "','" + p.problem[i] + "','" + p.problemDate[i]  + "')", connection);
                    cmd5.ExecuteNonQuery();
                }
            }

            SqlCommand cmd6 = new SqlCommand("INSERT INTO UserData values('" + p.user.id + "','" + p.user.password + "')", connection);
            cmd6.ExecuteNonQuery();

            if (p.test.Count != 0)
            {
                for (int i = 0; i < p.test.Count; i++ )
                {
                    System.Diagnostics.Debug.Print("tests added");
                    if (String.IsNullOrWhiteSpace(p.test[i].TID))
                    {
                        continue;
                    }
                    SqlCommand cmd3 = new SqlCommand("INSERT into PatientTest Values ('" + p.patient.PID + "','" + p.test[i].TID + "','" + p.testReadings[i] + "','" + p.testDate[i] + "')", connection);
                    cmd3.ExecuteNonQuery();
                }
            }

            if (p.medicine.Count != 0)
            {
                for (int i = 0; i < p.medicine.Count; i++)
                {
                    System.Diagnostics.Debug.Print("medicine added");
                    if (String.IsNullOrWhiteSpace(p.medicine[i].MID))
                    {
                        continue;
                    }
                    SqlCommand cmd4 = new SqlCommand("INSERT into CurrentPrescription Values ('" + p.patient.PID + "','" + p.medicine[i].MID + "','" + p.prescribedBy[i].DID + "','" + p.prescriptionDate[i] + "')", connection);
                    cmd4.ExecuteNonQuery();
                }
            }

            //if (p.allergies.allergyType != null)
            //{
            //    SqlCommand cmd2 = new SqlCommand("INSERT into Allergies Values ('" + p.allergies.AID + "','" + p.allergies.allergyType  + "')", connection);
            //    cmd2.ExecuteNonQuery();
            //}


            connection.Close();

            //return true;
        }

        public void editPatient(FullDetails p)
        {
            System.Diagnostics.Debug.Print("id=" + p.patient.PID);
            System.Diagnostics.Debug.Print("name=" + p.patient.name);
            System.Diagnostics.Debug.Print("length=" + p.allergy.Count);
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("update patient set Name='" + p.patient.name + "', DOB='" + p.patient.DOB + "', Gender='" + p.patient.gender + "', Address='" + p.patient.address + "', Phone='" + p.patient.phone + "', MarritalStatus='" + p.patient.marritalStatus + "', BloodGroup='" + p.patient.bloodGroup + "', FatherName='" + p.patient.fatherName + "', Occupation='" + p.patient.occupation + "', Height='" + p.patient.height + "', Weight='" + p.patient.weight + "' where PID='" + p.patient.PID + "'", connection);
            cmd1.ExecuteNonQuery();
            System.Diagnostics.Debug.Print("patient added" + p.patient.name);

            if (p.allergy.Count != 0)
            {
                for (int i = 0; i < p.allergy.Count; i++)
                {
                    System.Diagnostics.Debug.Print("allergydate=" + p.allergyDate[i]);
                    if (String.IsNullOrWhiteSpace(p.allergy[i].AID))
                    {
                        continue;
                    }
                    SqlCommand cmd2 = new SqlCommand("INSERT into PatientAllergy Values ('" + p.allergyDate[i] + "','" + p.patient.PID + "','" + p.allergy[i].AID + "')", connection);
                    cmd2.ExecuteNonQuery();
                }
            }

            if (p.problem.Count != 0)
            {
                for (int i = 0; i < p.problem.Count; i++)
                {
                    System.Diagnostics.Debug.Print("allergydate=" + p.allergyDate[i]);
                    if (String.IsNullOrWhiteSpace(p.problem[i]))
                    {
                        continue;
                    }
                    SqlCommand cmd5 = new SqlCommand("INSERT into Problems Values ('" + p.patient.PID + "','" + p.problem[i] + "','" + p.problemDate[i] + "')", connection);
                    cmd5.ExecuteNonQuery();
                }
            }

            if (p.test.Count != 0)
            {
                for (int i = 0; i < p.test.Count; i++)
                {
                    System.Diagnostics.Debug.Print("tests added");
                    if (String.IsNullOrWhiteSpace(p.test[i].TID))
                    {
                        continue;
                    }
                    SqlCommand cmd3 = new SqlCommand("INSERT into PatientTest Values ('" + p.patient.PID + "','" + p.test[i].TID + "','" + p.testReadings[i] + "','" + p.testDate[i] + "')", connection);
                    cmd3.ExecuteNonQuery();
                }
            }

            if (p.medicine.Count != 0)
            {
                for (int i = 0; i < p.medicine.Count; i++)
                {
                    System.Diagnostics.Debug.Print("medicine added");
                    if (String.IsNullOrWhiteSpace(p.medicine[i].MID))
                    {
                        continue;
                    }
                    SqlCommand cmd4 = new SqlCommand("INSERT into CurrentPrescription Values ('" + p.patient.PID + "','" + p.medicine[i].MID + "','" + p.prescribedBy[i].DID + "','" + p.prescriptionDate[i] + "')", connection);
                    cmd4.ExecuteNonQuery();
                }
            }
            connection.Close();
        }


        public FullDetails deletePatientAsk(string dlt)
        {
            FullDetails p = find(dlt);
            return p;
            //connection.Open();
            //System.Diagnostics.Debug.Print("" + dlt);
            //SqlCommand cmd = new SqlCommand("select * from patient where ID=" + dlt, connection);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //connection.Close();

            //patient p = new patient();
            //p.PID = Convert.ToInt32(dt.Rows[0]["ID"]);
            //p.name = Convert.ToString(dt.Rows[0]["Name"]);
            //p.DOB = Convert.ToString(dt.Rows[0]["DOB"]);
            //p.gender = Convert.ToString(dt.Rows[0]["Gender"]);
            //p.address = Convert.ToString(dt.Rows[0]["Address"]);
            //p.phone = Convert.ToString(dt.Rows[0]["Phone"]);
            //p.marritalStatus = Convert.ToString(dt.Rows[0]["MarritalStatus"]);
            //p.bloodGroup = Convert.ToString(dt.Rows[0]["BloodGroup"]);
            //p.fatherName = Convert.ToString(dt.Rows[0]["Father'sName"]);
            //p.occupation = Convert.ToString(dt.Rows[0]["Occupation"]);
            //p.height = Convert.ToString(dt.Rows[0]["Height"]);
            //p.weight = Convert.ToDouble(dt.Rows[0]["Weight"]);
            //System.Diagnostics.Debug.Print("" + p.name);
            

        }


        public FullDetails find(string str)
        {
            FullDetails p = new FullDetails();
            //SqlCommand cmd = new SqlCommand("select *  from Patient p inner join PatientAllergy pa on p.PID=pa.PID inner join Allergies a on pa.AID=a.AID inner join CurrentPrescription cp on p.PID = cp.PID inner join Medicine m on cp.MID = m.MID inner join PatientTest pt on p.PID = pt.PID inner join Doctor d on cp.PrescibedByDID= d.DID inner join Test t on t.TID= pt.TID where p.PID='" + str + "'" , connection);
            
            SqlCommand cmd1 = new SqlCommand("viewPatientDetails", connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@PID", str);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();

            connection.Open();
            da1.Fill(dt1);

            p.patient.name = Convert.ToString(dt1.Rows[0]["Name"]);
            System.Diagnostics.Debug.Print("name=" + p.patient.name);
            p.patient.PID = Convert.ToString(dt1.Rows[0]["PID"]);


            p.patient.DOB = Convert.ToString(dt1.Rows[0]["DOB"]);
            p.patient.gender = Convert.ToString(dt1.Rows[0]["Gender"]);
            p.patient.address = Convert.ToString(dt1.Rows[0]["Address"]);
            p.patient.phone = Convert.ToString(dt1.Rows[0]["Phone"]);
            p.patient.marritalStatus = Convert.ToString(dt1.Rows[0]["MarritalStatus"]);
            p.patient.bloodGroup = Convert.ToString(dt1.Rows[0]["BloodGroup"]);
            p.patient.fatherName = Convert.ToString(dt1.Rows[0]["FatherName"]);
            p.patient.occupation = Convert.ToString(dt1.Rows[0]["Occupation"]);
            p.patient.height = Convert.ToString(dt1.Rows[0]["Height"]);
            p.patient.weight = Convert.ToDouble(dt1.Rows[0]["Weight"]);
            p.patient.photo = Convert.ToString(dt1.Rows[0]["Photo"]);

            //Accessing allergies

            SqlCommand cmd2 = new SqlCommand("viewAllergyDetails", connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@PID", str);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();

            da2.Fill(dt2);

            foreach (DataRow row in dt2.Rows)
            {
                Allergies a= new Allergies();
                a.AID = Convert.ToString(row["AID"]);
                a.allergyType = Convert.ToString(row["AllergyType"]);
                p.allergy.Add(a);
                p.allergyDate.Add(Convert.ToString(row["DateOfAllergy"]));
                System.Diagnostics.Debug.Print("date=" + p.allergyDate[0]);
            }

            //Accessing problems

            SqlCommand cmd5 = new SqlCommand("viewProblemDetails", connection);
            cmd5.CommandType = CommandType.StoredProcedure;
            cmd5.Parameters.AddWithValue("@PID", str);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();

            da5.Fill(dt5);

            foreach (DataRow row in dt5.Rows)
            {
                p.problem.Add(Convert.ToString(row["problem"]));
                p.problemDate.Add(Convert.ToString(row["DateOfProblem"]));
                System.Diagnostics.Debug.Print("date=" + p.problemDate[0]);
            }

            //Accessing current prescriptions

            SqlCommand cmd3 = new SqlCommand("viewPrescriptionDetails", connection);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.AddWithValue("@PID", str);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();

            da3.Fill(dt3);

            foreach (DataRow row in dt3.Rows)
            {
                Medicine m = new Medicine();
                m.medicineName = Convert.ToString(row["MedicineName"]);
                System.Diagnostics.Debug.Print("medicine=" + m.medicineName);
                m.MID = Convert.ToString(row["MID"]);
                p.medicine.Add(m);
                Doctor d = new Doctor();
                d.doctorName = Convert.ToString(row["DoctorName"]);
                p.prescribedBy.Add(d);
                p.prescriptionDate.Add(Convert.ToString(row["Date"]));
                System.Diagnostics.Debug.Print("date=" + p.prescriptionDate[0]);
            }

            //Accessing Tests Taken

            SqlCommand cmd4 = new SqlCommand("viewTestDetails", connection);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.AddWithValue("@PID", str);
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            foreach (DataRow row in dt4.Rows)
            {
                Test t = new Test();
                t.TID = Convert.ToString(row["TID"]);
                System.Diagnostics.Debug.Print("medicine=" + t.TID);
                t.testName = Convert.ToString(row["TestName"]);

                p.test.Add(t);
                p.testDate.Add(Convert.ToString(row["DateOfTest"]));
                System.Diagnostics.Debug.Print("date=" + p.testDate[0]);
                p.testReadings.Add(Convert.ToString(row["Result"]));
                System.Diagnostics.Debug.Print("date=" + p.testReadings[0]);
            }
            connection.Close();
            return p;
        }


        public int deletePatient(Patient dlt)
        {
            connection.Open();
            System.Diagnostics.Debug.Print("" + dlt);
            SqlCommand cmd = new SqlCommand("delete from patient where ID='" + dlt.PID+ "'", connection);
            int dltFlag = cmd.ExecuteNonQuery();
            return dltFlag;


        }

        public List<SelectListItem> getAllergyItems()
        {
            SqlCommand cmd = new SqlCommand("select * from Allergies order by AllergyType asc", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<SelectListItem> l = new List<SelectListItem>();
            foreach (DataRow rows in dt.Rows)
            {
                l.Add(new SelectListItem
                {
                    Value = Convert.ToString(rows["AID"]),
                    Text=Convert.ToString(rows["AllergyType"])
                });
            }
            connection.Close();

            return l;
        }

        public List<SelectListItem> getMedicineItems()
        {
            SqlCommand cmd = new SqlCommand("select * from Medicine order by MedicineName asc", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<SelectListItem> l = new List<SelectListItem>();
            foreach (DataRow rows in dt.Rows)
            {
                l.Add(new SelectListItem
                {
                    Value = Convert.ToString(rows["MID"]),
                    Text = Convert.ToString(rows["MedicineName"])
                });
            }
            connection.Close();

            return l;
        }

        public List<SelectListItem> getTestItems()
        {
            SqlCommand cmd = new SqlCommand("select * from Test order by TestName asc", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<SelectListItem> l = new List<SelectListItem>();
            foreach (DataRow rows in dt.Rows)
            {
                l.Add(new SelectListItem
                {
                    Value = Convert.ToString(rows["TID"]),
                    Text = Convert.ToString(rows["TestName"])
                });
            }
            connection.Close();

            return l;
        }

       

        public List<SelectListItem> getDoctorItems()
        {
            SqlCommand cmd = new SqlCommand("select * from Doctor order by DoctorName asc", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<SelectListItem> l = new List<SelectListItem>();
            foreach (DataRow rows in dt.Rows)
            {
                l.Add(new SelectListItem
                {
                    Value = Convert.ToString(rows["DID"]),
                    Text = Convert.ToString(rows["DoctorName"])
                });
            }
            connection.Close();

            return l;
        }
    }
}