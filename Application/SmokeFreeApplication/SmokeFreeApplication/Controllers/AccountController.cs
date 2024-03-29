﻿using SmokeFreeApplication.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace SmokeFreeApplication.Controllers
{
    public class AccountController : Controller
    {

        private SmokeFreeDBContext db = new SmokeFreeDBContext();
        private string GetMD5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(password);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

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

        [HttpPost]
        public ActionResult SignUpAsMember(InterestedPartyCompoundModel accountInfo)
        {
            if (ModelState.IsValid)
            {
                var checkUsername = db.GeneralUser.FirstOrDefault(m => m.userName == accountInfo.GeneralUsers.userName);
                var checkEmail = db.GeneralUser.FirstOrDefault(m => m.email == accountInfo.GeneralUsers.email);

                //Convert uploaded file to byte[]
                HttpPostedFileBase postedFile = Request.Files["ImageFile"];
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] img = binaryReader.ReadBytes((int)stream.Length);

                //Check if username & email already exists
                if (checkUsername == null)
                {
                    if (checkEmail == null)
                    {
                        //Set details
                        accountInfo.GeneralUsers.password = GetMD5(accountInfo.GeneralUsers.password);
                        accountInfo.GeneralUsers.profilePicture = img;
                        accountInfo.InterestedParties.userName = accountInfo.GeneralUsers.userName;
                        accountInfo.InterestedParties.bio = "";

                        //Insert into database
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.GeneralUser.Add(accountInfo.GeneralUsers);
                        db.InterestedParty.Add(accountInfo.InterestedParties);
                        db.SaveChanges();
                        return RedirectToAction("SignUpSuccessMember");
                    }
                    else
                    {
                        ViewData["EmailExists"] = "Email already exists";
                    }
                }
                else
                {
                    ViewData["UsernameExists"] = "Username already exists";
                }
            }
            return View("SignUpMember");
        }

        [HttpPost]
        public ActionResult SignUpAsDoctor(DoctorCompoundModel accountInfo)
        {
            if (ModelState.IsValid)
            {
                var checkUsername = db.GeneralUser.FirstOrDefault(m => m.userName == accountInfo.GeneralUsers.userName);
                var checkEmail = db.GeneralUser.FirstOrDefault(m => m.email == accountInfo.GeneralUsers.email);

                //Convert uploaded file to byte[]
                HttpPostedFileBase postedFile = Request.Files["ImageFile"];
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] img = binaryReader.ReadBytes((int)stream.Length);

                //Check if username & email already exists
                if (checkUsername == null)
                {
                    if (checkEmail == null)
                    {
                        //Set details
                        accountInfo.GeneralUsers.password = GetMD5(accountInfo.GeneralUsers.password);
                        accountInfo.GeneralUsers.profilePicture = img;
                        accountInfo.Doctors.adminVerify = false;
                        accountInfo.Doctors.description = "";
                        accountInfo.Doctors.userName = accountInfo.GeneralUsers.userName;

                        //Insert into database
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.GeneralUser.Add(accountInfo.GeneralUsers);
                        db.Doctor.Add(accountInfo.Doctors);
                        db.SaveChanges();
                        return RedirectToAction("SignUpSuccessDoctor");
                    }
                    else
                    {
                        ViewData["EmailExists"] = "Email already exists";
                    }
                }
                else
                {
                    ViewData["UsernameExists"] = "Username already exists";
                }
            }
            return View("SignUpDoctor");
        }

        public ActionResult SignUpSuccessMember()
        {

            return View();
        }

        public ActionResult SignUpSuccessDoctor()
        {

            return View();
        }

        public ActionResult SignIn()
        {

            return View();
        }

        public ActionResult SignInAdmin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignningIn(GeneralUser accountInfo)
        {

            var fPassword = GetMD5(accountInfo.password);
            var data = db.GeneralUser.Where(s => s.userName.Equals(accountInfo.userName) && s.password.Equals(fPassword)).ToList();
            var checkDoc = db.Doctor.Where(s => s.userName.Equals(accountInfo.userName)).ToList();

            if (data.Count() > 0)
            {
                var username = data.FirstOrDefault().userName;
                var doctorRecord = db.Doctor.Find(username);

                //Check if it is a doctor account
                if (checkDoc.Count() > 0)
                {
                    //Check if doctor account has been verified
                    if (doctorRecord.adminVerify == true)
                    {
                        //Store username in session
                        Session["username"] = username;
                        Session["docOrMember"] = "doc";
                        //Set error messages to null
                        TempData["loginFailed"] = "";
                        TempData["notVerified"] = "";
                        return RedirectToAction("Articles", "Article");
                    }
                    //Else display error message
                    else
                    {
                        TempData["notVerified"] = "Your account has not been verified by an adminstrator yet. Please give it at least 2 working days.";
                        return RedirectToAction("SignIn");
                    }

                }
                //Else if interested party account
                else
                {
                    //Store username in session
                    Session["username"] = data.FirstOrDefault().userName;
                    Session["docOrMember"] = "member";
                    //Set error messages to null
                    TempData["loginFailed"] = "";
                    return RedirectToAction("Stories", "Story");
                }
            }
            else
            {
                TempData["loginFailed"] = "Wrong username/password, please try again";
                return RedirectToAction("SignIn");
            }

        }
       
        public ActionResult SignInAsAdmin(Admin accountInfo)
        {
            var fPassword = GetMD5(accountInfo.password);
            var data = db.Admin.Where(s => s.id.Equals(accountInfo.id) && s.password.Equals(fPassword)).ToList();

            if (data.Count() > 0)
            {
                var username = data.FirstOrDefault().id;

                //Store username in session
                Session["username"] = username;
                Session["docOrMember"] = "admin";

                //Set error messages to null
                TempData["loginFailed"] = "";
                TempData["notVerified"] = "";
                return RedirectToAction("Manage", "Admin");
            }
            else
            {
                TempData["loginFailed"] = "Wrong username/password, please try again!";
                return RedirectToAction("SignInAdmin");
            }

        }

        public ActionResult Logout()
        {
            //Clear Session
            Session.Clear();
            //Return to homepage
            return RedirectToAction("Index", "Home");
        }
    }
}