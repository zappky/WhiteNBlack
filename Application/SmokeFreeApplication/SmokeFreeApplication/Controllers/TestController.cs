using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SmokeFreeApplication.Models;

//just for me to test things out

namespace SmokeFreeApplication.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ViewResult Test()
        {
            var test = new Models.Test() { Name = "Test",ID=1};
            



            return View(test);
        }
        public ActionResult printTest()
        {
            DocEntry aUser = new DocBuilder()
            .UserBio("hahadoc", new DateTime(), "femaleDoc")
            .UserMeta("userHahaDoc", "abe@hotmail.com", "kekeke", "something")
            .DocInfo("ntu", "fake", 999, "999", true)
            .Build();

            UserBio genericBio = new UserBio() { name = "KK", dateOfBirth = new DateTime(), gender = "Male" };
            UserMeta genericUserMeta =  new UserMeta(){ userName ="KK", email = "abc@hotmail.com",  password="KK",  profilePicture ="Oma wa mou shindeiru"};
            Admin aAdmin = new AdminBuilder()
                .UserBio(genericBio)
                .UserMeta(genericUserMeta)
                .AdminInfo("1")
                .Build();


            return View(aUser);
        }
        public ActionResult Index()
        {
            var test = new Models.Test() { Name = "Index", ID = 1 };
            return View(test);
        }

        //?id=something&name=something
        public ActionResult para(int id,string name )
        {
            var test = new Models.Test() { Name = name, ID = id };
            return View(test);
        }

        public ActionResult redirect()
        {
            var test = new Models.Test() { Name = "redirect", ID = 1 };
            return RedirectToAction("para", "Test",new { id =99, name = "redirected"});
        }
    }
}