using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmokeFreeApplication.Models;
using PagedList;

namespace SmokeFreeApplication.Controllers
{
    public class InterestedPartyController : Controller
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        // GET: InterestedParty
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemberProfile(string viewUsername, int? page)
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            List<Story> displayList = new List<Story>();

            if (Session["username"] == null) 
            {
                return RedirectToAction("SignInMember", "Account");
            }
            else 
            {
                string username = Session["username"].ToString();
                var generaldata = smokeFreeDB.GeneralUser.Where(x => x.userName.Equals(viewUsername));
                var interestedpartydata = smokeFreeDB.InterestedParty.Where(x => x.userName.Equals(viewUsername));

                displayList = smokeFreeDB.Story.Where(x => x.userName.Equals(viewUsername)).ToList();
                ViewBag.memberName = generaldata.FirstOrDefault().name;
                ViewBag.memberBio = interestedpartydata.FirstOrDefault().bio;
                ViewBag.username = viewUsername;

                if (username == viewUsername)
                {
                    ViewBag.ownAccount = true;
                }
                else
                {
                    ViewBag.ownAccount = false;
                }
            }

            return View(displayList.ToPagedList(pageNumber, pageSize));
        }

        public FileContentResult RetrieveMemberPic(string username)
        {
            byte[] imgByteArray = smokeFreeDB.GeneralUser.Find(username).profilePicture;
            if (imgByteArray != null)
            {
                return new FileContentResult(imgByteArray, "image/jpeg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult EditMemberProfile()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignInMember", "Account");
            }
            else
            {
                string username = Session["username"].ToString();
                var generaldata = smokeFreeDB.GeneralUser.Where(x => x.userName.Equals(username));
                var interestedPartydata = smokeFreeDB.InterestedParty.Where(x => x.userName.Equals(username));

                ViewBag.smokerOrNot = interestedPartydata.FirstOrDefault().smokerOrNot;
                ViewBag.bio = interestedPartydata.FirstOrDefault().bio;
                ViewBag.username = username;

            }
            return View();
        }
        public ActionResult EditedMemberProfile(InterestedPartyCompoundModel accountInfo)
        {
            string username = Session["username"].ToString();
            var member = smokeFreeDB.InterestedParty.Find(username);
            member.bio = accountInfo.InterestedParties.bio;
            member.smokerOrNot = accountInfo.InterestedParties.smokerOrNot;

            smokeFreeDB.SaveChanges();

            return RedirectToAction("MemberProfile", new { viewUsername = username, page = 1 });
        }
    }
}