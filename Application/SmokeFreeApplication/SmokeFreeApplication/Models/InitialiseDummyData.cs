using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class InitialiseDummyData
    {
        public static void Initialize()
        {
            using (var context = new SmokeFreeDBContext())
            {
                // Look for any GeneralUser
                if (context.GeneralUser.Any())
                {
                    return;   // DB has been seeded
                }
                else
                {
                    // Create a dummy doctor
                    context.GeneralUser.Add(
                        new GeneralUser
                        {
                            userName = "generaluser1",
                            name = "General User",
                            email = "generaluser1@test.com",
                            password = "generaluser1",
                            confirmPassword = "generaluser1",
                            dateOfBirth = DateTime.Now,
                            gender = "Male",
                            profilePicture = ""
                        }
                    );
                }
                // Hi, if anyone know how to input doctor data automatically, pls help thnx -- Daniel
                //
                //if (context.Doctor.Any())
                //{
                //    return;   // DB has been seeded
                //}
                //else
                //{
                //    context.Doctor.Add(
                //        new DoctorCompoundModel
                //        {
                //            userName = "doctor1",
                //            workLocation = "Test Hospital",
                //            description = "I'm a test doctor. Test test doctor test.",
                //            contactNo = 12345678,
                //            doctorID = "12345",
                //            adminVerify = true,

                //            email = "generaluser1@test.com",
                //            password = "generaluser1",
                //            confirmPassword = "generaluser1",
                //            dateOfBirth = DateTime.Now,
                //            gender = "Male",
                //            profilePicture = ""
                //        }
                //    );
                //}
                context.SaveChanges();
            }
        }
    }
}