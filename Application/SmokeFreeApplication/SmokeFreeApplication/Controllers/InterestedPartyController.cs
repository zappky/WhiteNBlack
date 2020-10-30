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
                return RedirectToAction("SignIn", "Account");
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
                ViewBag.smokerOrNot = interestedpartydata.FirstOrDefault().smokerOrNot;

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
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                string username = Session["username"].ToString();
                var generaldata = smokeFreeDB.GeneralUser.Where(x => x.userName.Equals(username));
                var interestedPartydata = smokeFreeDB.InterestedParty.Where(x => x.userName.Equals(username));

                //ViewBag.smokerOrNot = interestedPartydata.FirstOrDefault().smokerOrNot;
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
            //member.smokerOrNot = accountInfo.InterestedParties.smokerOrNot;

            smokeFreeDB.SaveChanges();

            return RedirectToAction("MemberProfile", new { viewUsername = username, page = 1 });
        }

        // Track Progression actions
        public ActionResult FillInDetails()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                ViewBag.trackUsername = Session["username"];
            }

            return View();
        }
        [HttpPost]
        public ActionResult FillInDetails(Progress progressInfo)
        {
            //Save details in database
            if (ModelState.IsValid)
            {
                smokeFreeDB.Configuration.ValidateOnSaveEnabled = false;
                smokeFreeDB.Progress.Add(progressInfo);
                smokeFreeDB.SaveChanges();
            }

            return RedirectToAction("TrackProgress");
        }

        public ActionResult TrackProgress()
        {
            // If not logged in, redirect to the sign in page
            if (Session["username"] == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            
            ProgressViewModel progView = new ProgressViewModel();
            Progress progress = new Progress();
            string user = Session["username"].ToString();

            // If has no progress, direct to another page for user to fill in details.
            var checkUsername = smokeFreeDB.Progress.FirstOrDefault(m => m.userName == user);
            if (checkUsername == null)
            {
                return RedirectToAction("FillInDetails");
            }
            else
            {
                // query for the correct progress
                progress = smokeFreeDB.Progress.Find(user);

                // query for all checkins
                List<CheckInDate> checkins = new List<CheckInDate>();
                checkins = smokeFreeDB.CheckInDate.Where(c => c.userName == user).OrderByDescending(c => c.checkInDate).ToList();
                int totalCheckins = checkins.Count();

                // query for user's name
                string user_realname = smokeFreeDB.GeneralUser.Where(c => c.userName == user).FirstOrDefault().name;
                // pass out the calculated display
                progView.userName = user_realname;
                progView.streak = getStreak(checkins);
                progView.totalCheck = totalCheckins;
                progView.cigaSaved = progress.cigaIntake * totalCheckins;
                progView.cashSaved = (progress.cigaPrice / 20) * progView.cigaSaved * totalCheckins;
            }

            return View(progView);
        }

        public int getStreak(List<CheckInDate> c)
        {
            int i = 0;
            // If user has no checkins yet, streak is 0
            if(c.Count() < 2)
            {
                return c.Count();
            }
            // If today or yesterday has a checkin, do a regular count
            if ((c[i].checkInDate == DateTime.Today) || (c[i].checkInDate == DateTime.Today.AddDays(-1)))
            {
                // compare each check in date to ensure that they are consecutive days
                while ((i < c.Count()-1))
                {
                    if(c[i].checkInDate.AddDays(-1) != c[i + 1].checkInDate)
                    {
                        // If not consecutive, that is where the streak has started
                        break;
                    }
                    i++;
                }
                i++;
                return i;
            }
            // If today and yesterday have no checkin, streak ended.
            else
            {
                return 0;
            }
            
        }

        [HttpPost]
        public ActionResult CheckIn([Bind(Include = "checkInId,checkInDate,userName")] CheckInDate check)
        {
            string user = Session["username"].ToString();
            DateTime tod = DateTime.Now.Date;
            
            if (smokeFreeDB.CheckInDate.Where(c => c.userName == user && c.checkInDate == check.checkInDate).FirstOrDefault() != null)
            {
                // If it is duplicate, refresh and do nothing
                return Redirect("~/InterestedParty/TrackProgress");
            }
            else if (check.checkInDate > tod)
            {
                // If check in is for a future date, also don't save
                return Redirect("~/InterestedParty/TrackProgress");
            }
            else
            {
                // Else, save the new check in and update smokeFreeDB.Progress
                smokeFreeDB.Progress.Where(c => c.userName == user).FirstOrDefault().totalCheckins += 1;
                smokeFreeDB.CheckInDate.Add(check);
                smokeFreeDB.SaveChanges();
            }

            return Redirect("~/InterestedParty/TrackProgress");
        }

        public JsonResult GetUserCheckIn()
        {
            var viewModel = new CheckInViewModel();
            var events = new List<CheckInViewModel>();
            //Get check ins
            string user = Session["username"].ToString();
            List<CheckInDate> checkins = new List<CheckInDate>();
            checkins = smokeFreeDB.CheckInDate.Where(c => c.userName == user).ToList();

            for (var i = 0; i < checkins.Count(); i++)
            {
                var startDateStr = checkins[i].checkInDate.Date.ToString("yyyy-MM-dd");
                var endDateStr = checkins[i].checkInDate.AddDays(1).Date.ToString("yyyy-MM-dd");
                events.Add(new CheckInViewModel()
                {
                    startStr = startDateStr,
                    endStr = endDateStr,
                    allDay = true,
                    display = "background",
                    color = "#d1ffc9"
                });

            }


            return new JsonResult { Data = events.ToArray(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        } 
    }
}