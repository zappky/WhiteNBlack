using SmokeFreeApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SmokeFreeApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult SignUpMember()
        {
            ViewBag.Message = "Sign up as a member here";
            return View();
        }

        public ActionResult SignUpDoctor()
        {
           
            ViewBag.Message = "Sign up as a doctor here";
            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Sign in here";
            return View();
        }

        [HttpPost]
        public ActionResult SignUpAsMember(InterestedParty accountInfo)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

          /*  HttpPostedFile postedFile = FileUpload.PostedFile;
            Stream stream = postedFile.InputStream;
            BinaryReader binaryReader = new BinaryReader(stream);*/
            
            
            string queryUser = "INSERT INTO GeneralUser(userName, name, email, password, dateOfBirth, gender) VALUES(@userName, @name, @email, @password, @dateOfBirth, @gender)";
            string queryInterestedParty = "INSERT INTO InterestedParty(userName, smokerOrNot, bio) VALUES(@userName, @smokerOrNot, @bio)";

            con.Open();
            SqlCommand cmdU = new SqlCommand(queryUser, con);
            cmdU.Parameters.AddWithValue("@userName", accountInfo.userName);
            cmdU.Parameters.AddWithValue("@name", accountInfo.name);
            cmdU.Parameters.AddWithValue("@email", accountInfo.email);
            cmdU.Parameters.AddWithValue("@password", accountInfo.password);
            cmdU.Parameters.AddWithValue("@dateOfBirth", accountInfo.dateOfBirth);
            cmdU.Parameters.AddWithValue("@gender", accountInfo.gender);
/*            cmdU.Parameters.AddWithValue("@profilePicture", null);*/
            cmdU.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmdIP = new SqlCommand(queryInterestedParty, con);
            cmdIP.Parameters.AddWithValue("@userName", accountInfo.userName);
            cmdIP.Parameters.AddWithValue("@smokerOrNot", Convert.ToBoolean(accountInfo.smokerOrNot));
            cmdIP.Parameters.AddWithValue("@bio", "");
            cmdIP.ExecuteNonQuery();
            con.Close();

            
            return View("SignUpMember");
        }

        public ActionResult SignUpAsDoctor(Doctor accountInfo)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);


            string queryUser = "INSERT INTO GeneralUser(userName, name, email, password, dateOfBirth, gender) VALUES(@userName, @name, @email, @password, @dateOfBirth, @gender)";
            string queryDoctor = "INSERT INTO Doctor(userName, workLocation, description, contactNo, doctorID, adminVerify) VALUES(@userName, @workLocation, @description, @contactNo, @doctorID, @adminVerify)";

            con.Open();
            SqlCommand cmdU = new SqlCommand(queryUser, con);
            cmdU.Parameters.AddWithValue("@userName", accountInfo.userName);
            cmdU.Parameters.AddWithValue("@name", accountInfo.name);
            cmdU.Parameters.AddWithValue("@email", accountInfo.email);
            cmdU.Parameters.AddWithValue("@password", accountInfo.password);
            cmdU.Parameters.AddWithValue("@dateOfBirth", accountInfo.dateOfBirth);
            cmdU.Parameters.AddWithValue("@gender", accountInfo.gender);
            /*            cmdU.Parameters.AddWithValue("@profilePicture", null);*/
            cmdU.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmdD = new SqlCommand(queryDoctor, con);
            cmdD.Parameters.AddWithValue("@userName", accountInfo.userName);
            cmdD.Parameters.AddWithValue("@workLocation", accountInfo.workLocation);
            cmdD.Parameters.AddWithValue("@description", "");
            cmdD.Parameters.AddWithValue("@contactNo", accountInfo.contactNo);
            cmdD.Parameters.AddWithValue("@doctorID", accountInfo.doctorID);
            cmdD.Parameters.AddWithValue("@adminVerify", false);
            cmdD.ExecuteNonQuery();
            con.Close();

            return View("SignUpDoctor");
        }

        public ActionResult SignInAcc(GeneralUser accountInfo)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            string query = "SELECT * FROM GeneralUser WHERE userName = @userName AND password = @password";

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userName", accountInfo.userName);
            cmd.Parameters.AddWithValue("@password", accountInfo.password);

            object loginVal = cmd.ExecuteScalar();

            if(loginVal != null)
            {
                Session["username"] = accountInfo.ToString();
                return View("../Story/Stories");
            }
            else
            {
                TempData["message"] = "Wrong username/password, please try again";
                return View("SignIn");

            }
           
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("../Home/Index");

        }
    }
}