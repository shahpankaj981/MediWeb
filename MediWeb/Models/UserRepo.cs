using MediWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MediWeb.Models
{
    public class UserRepo
    {

        public User getUserDetails(User user)
        {
            if (Exists(user))
            {
                return user;
            }

            return null;
        }

        public bool Exists(User user)
        {
            //check for user credentials
            //get id, pass from database; check with user

            if (!String.IsNullOrEmpty(user.id) || !String.IsNullOrEmpty(user.password))
            {

                //sql connection open
                patientRepository pr = new patientRepository();
                User u = pr.getUser(user);
                //retrieve user data from database

                string patient_id = u.id;
                string password = u.password;

                if (user.id.ToLower() == patient_id.ToLower() && user.password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}