namespace SmokeFreeApplication.Migrations
{
    using SmokeFreeApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<SmokeFreeApplication.Models.SmokeFreeDBContext>
    {
    
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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
        private string getImagePath()
        {
            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath; //was AbsolutePath but didn't work with spaces according to comments
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + "~/images/dummy_profile.png".TrimStart('~').Replace('/', '\\'));
            return path;
        }

        protected override void Seed(SmokeFreeApplication.Models.SmokeFreeDBContext context)
        {
            

            //Auto populate database whenever data gets wiped. Will insert if data has not been inserted yet
            context.Admin.AddOrUpdate(
                p => p.id,
                new Admin 
                { 
                    id = "adminAcc", 
                    password = GetMD5("adminpw123") 
                },
                new Admin 
                { 
                    id = "adminAcc2", 
                    password = GetMD5("adminpw123")
                }
                );

        

           context.GeneralUser.AddOrUpdate(
               p => p.userName,
               new GeneralUser
               {
                   userName = "generaluser1",
                   name = "Interested Party 1",
                   password = GetMD5("generaluser1"),
                   dateOfBirth = DateTime.Parse("1990-1-11"),
                   email = "generaluser@test.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath()),
                   confirmPassword = GetMD5("generaluser1")
               },
               new GeneralUser
               {
                   userName = "generaluser2",
                   name = "Interested Party 2",
                   password = GetMD5("generaluser2"),
                   dateOfBirth = DateTime.Parse("1991-2-22"),
                   email = "12321@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath()),
                   confirmPassword = GetMD5("generaluser2")
               },
               new GeneralUser
               {
                   userName = "doctor1",
                   name = "Doctor 1",
                   password = GetMD5("doctor123"),
                   dateOfBirth = DateTime.Parse("1994-5-22"),
                   email = "doctor1@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath()),
                   confirmPassword = GetMD5("doctor123")
               },
               new GeneralUser
               {
                   userName = "doctor2",
                   name = "Doctor 2",
                   password = GetMD5("doctor321"),
                   dateOfBirth = DateTime.Parse("1990-12-1"),
                   email = "doctor2@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath()),
                   confirmPassword = GetMD5("doctor321")
               }
               );

          
           
            context.InterestedParty.AddOrUpdate(
                p => p.userName,
                new InterestedParty
                {
                    userName = "generaluser1",
                    smokerOrNot = false,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "generaluser2",
                    smokerOrNot = true,
                    bio = ""
                }

                );

            context.Doctor.AddOrUpdate(
                p => p.userName,
                new Doctor
                {
                    userName = "doctor1",
                    workLocation = "Tan Tock Seng Hospital",
                    description = "",
                    contactNo = 61234567,
                    doctorID = "M12345A",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "doctor2",
                    workLocation = "National University Hospital",
                    description = "",
                    contactNo = 61112222,
                    doctorID = "M54553P",
                    adminVerify = true
                }
                );

        }
    }
}
