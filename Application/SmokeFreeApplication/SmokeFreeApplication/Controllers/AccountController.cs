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
using System.Data.Entity;
using System.Web.Hosting;
using System.Security.Cryptography;
using System.Text;
using System.Dynamic;

namespace SmokeFreeApplication.Controllers
{
    public class AccountController : Controller
    {
        private SmokeFreeDBContext db = new SmokeFreeDBContext();
        // GET: Login
        public ActionResult SignUpMember()
        {
            ViewBag.Message = "Sign up as a member here";
            return View();
        }

        public ActionResult SignUpDoctor()
        {
            DoctorCompoundModel docModel = new DoctorCompoundModel();

            ViewBag.Message = "Sign up as a doctor here";
            return View(docModel);
        }

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

        [HttpPost]
        public ActionResult SignUpAsMember(GeneralUser accountInfo)
        {
            if (ModelState.IsValid)
            {
                var checkUsername = db.GeneralUser.FirstOrDefault(m => m.userName == accountInfo.userName);
                var checkEmail = db.GeneralUser.FirstOrDefault(m => m.email == accountInfo.email);
                if (checkUsername == null)
                {
                    if (checkEmail == null)
                    {
                        accountInfo.password = GetMD5(accountInfo.password);
                        InterestedParty intParty = new InterestedParty(accountInfo.userName, false, "");

                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.GeneralUser.Add(accountInfo);
                        db.InterestedParty.Add(intParty);
                        db.SaveChanges();
                        return RedirectToAction("SignInMember");
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
                if (checkUsername == null)
                {
                    if (checkEmail == null)
                    {
                        accountInfo.GeneralUsers.password = GetMD5(accountInfo.GeneralUsers.password);
                        accountInfo.Doctors.adminVerify = false;
                        accountInfo.Doctors.description = "";
                        accountInfo.Doctors.userName = accountInfo.GeneralUsers.userName;

                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.GeneralUser.Add(accountInfo.GeneralUsers);
                        db.Doctor.Add(accountInfo.Doctors);
                        db.SaveChanges();
                        return RedirectToAction("SignInDoctor");
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

        public ActionResult SignInMember()
        {
            return View();
        }
        public ActionResult SignInDoctor()
        {
            return View();
        }
        public ActionResult SignInAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignInAsMember(GeneralUser accountInfo)
        {

            var fPassword = GetMD5(accountInfo.password);
            var data = db.GeneralUser.Where(s => s.userName.Equals(accountInfo.userName) && s.password.Equals(fPassword)).ToList();
            if (data.Count() > 0)
            {
                //Store username in session
                Session["username"] = data.FirstOrDefault().userName;
                Session["docOrMember"] = "member";
                TempData["loginFailed"] = "";
                return View("../Story/Stories");
            }
            else
            {
                TempData["loginFailed"] = "Login Failed";
                return RedirectToAction("SignInMember");
            }



        }
        public ActionResult SignInAsDoctor(GeneralUser accountInfo)
        {
            var fPassword = GetMD5(accountInfo.password);
            var data = db.GeneralUser.Where(s => s.userName.Equals(accountInfo.userName) && s.password.Equals(fPassword)).ToList();

            if (data.Count() > 0)
            {
                var username = data.FirstOrDefault().userName;
                var doctorRecord = db.Doctor.Find(username);
                if (doctorRecord.adminVerify == true)
                {
                    //Store username in session
                    Session["username"] = username;
                    Session["docOrMember"] = "doc";
                    //Set error messages to null
                    TempData["loginFailed"] = "";
                    TempData["notVerified"] = "";
                    return View("../Article/Articles");
                }
                else
                {
                    TempData["notVerified"] = "Your account has not been verified by an adminstrator yet. Please give it 2 working days.";
                    return RedirectToAction("SignInDoctor");
                }

            }
            else
            {
                TempData["loginFailed"] = "Login Failed";
                return RedirectToAction("SignInDoctor");
            }
        }

        public ActionResult SignInAsAdmin(InterestedParty accountInfo)
        {
            TempData["signInAdminMessage"] = "Wrong username/password, please try again";
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return View("../Home/Index");

        }
    }
}