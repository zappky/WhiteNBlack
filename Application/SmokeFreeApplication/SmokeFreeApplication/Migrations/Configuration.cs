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
        private string getImagePath(string imgPath)
        {
            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath; //was AbsolutePath but didn't work with spaces according to comments
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + imgPath.TrimStart('~').Replace('/', '\\'));
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
                   userName = "Edna12",
                   name = "Edna",
                   password = GetMD5("password123"),
                   dateOfBirth = DateTime.Parse("1950-7-7"),
                   email = "edna12@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Edna.jpg")),
                   confirmPassword = GetMD5("password123")
               },
               new GeneralUser
               {
                   userName = "mm233",
                   name = "Margaret",
                   password = GetMD5("123password"),
                   dateOfBirth = DateTime.Parse("1932-1-23"),
                   email = "mm23@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Margaret.jpg")),
                   confirmPassword = GetMD5("123password")
               },
               new GeneralUser
               {
                   userName = "royyy13",
                   name = "Leroy",
                   password = GetMD5("123password321"),
                   dateOfBirth = DateTime.Parse("1924-12-25"),
                   email = "royyy13@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/leroy.jpg")),
                   confirmPassword = GetMD5("123password321")
               },
               new GeneralUser
               {
                   userName = "sssusan11",
                   name = "Susan",
                   password = GetMD5("4321p@assword"),
                   dateOfBirth = DateTime.Parse("1950-3-11"),
                   email = "sssusan11@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Susan.jpg")),
                   confirmPassword = GetMD5("4321p@assword")
               },
               new GeneralUser
               {
                   userName = "jj665",
                   name = "Joan",
                   password = GetMD5("123p@ssword"),
                   dateOfBirth = DateTime.Parse("1946-4-1"),
                   email = "jj66@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Joan.jpg")),
                   confirmPassword = GetMD5("123p@ssword")
               },
               new GeneralUser
               {
                   userName = "pete99",
                   name = "Peter",
                   password = GetMD5("4545pwpw"),
                   dateOfBirth = DateTime.Parse("1958-8-28"),
                   email = "pete99@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Peter.jpg")),
                   confirmPassword = GetMD5("4545pwpw")
               },
               new GeneralUser
               {
                   userName = "jamesbond",
                   name = "James",
                   password = GetMD5("007pass700"),
                   dateOfBirth = DateTime.Parse("1944-11-16"),
                   email = "jamesbond@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/James.jpg")),
                   confirmPassword = GetMD5("007pass700")
               },
               new GeneralUser
               {
                   userName = "dddd01",
                   name = "David",
                   password = GetMD5("dav452pass"),
                   dateOfBirth = DateTime.Parse("1960-1-1"),
                   email = "dddd01@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/David.jpg")),
                   confirmPassword = GetMD5("dav452pass")
               },
               new GeneralUser
               {
                   userName = "lis45",
                   name = "Lisa",
                   password = GetMD5("li4password"),
                   dateOfBirth = DateTime.Parse("1973-6-3"),
                   email = "lis4@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Lisa.jpg")),
                   confirmPassword = GetMD5("li4password")
               },
               new GeneralUser
               {
                   userName = "Mike9",
                   name = "Michael",
                   password = GetMD5("ekim9pass"),
                   dateOfBirth = DateTime.Parse("1966-3-14"),
                   email = "Mike9@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Michael.jpg")),
                   confirmPassword = GetMD5("ekim9pass")
               },
               new GeneralUser
               {
                   userName = "Jayyy13",
                   name = "Jason",
                   password = GetMD5("123p@ssword@"),
                   dateOfBirth = DateTime.Parse("1982-9-13"),
                   email = "Jayyy13@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Jason.jpg")),
                   confirmPassword = GetMD5("123p@ssword@")
               },
               new GeneralUser
               {
                   userName = "jo6na",
                   name = "John",
                   password = GetMD5("onjo6123"),
                   dateOfBirth = DateTime.Parse("1977-7-7"),
                   email = "jo6n@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/John.jpg")),
                   confirmPassword = GetMD5("onjo6123")
               },
               new GeneralUser
               {
                   userName = "r4h4m",
                   name = "Raham",
                   password = GetMD5("rahapr44"),
                   dateOfBirth = DateTime.Parse("1980-4-4"),
                   email = "r4h4m@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Raham.jpg")),
                   confirmPassword = GetMD5("rahapr44")
               },
               new GeneralUser
               {
                   userName = "dan77",
                   name = "Dan",
                   password = GetMD5("dandandan1"),
                   dateOfBirth = DateTime.Parse("1977-7-8"),
                   email = "dan77@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Dan.jpg")),
                   confirmPassword = GetMD5("dandandan1")
               },
               new GeneralUser
               {
                   userName = "ph111",
                   name = "Phil",
                   password = GetMD5("phil8611"),
                   dateOfBirth = DateTime.Parse("1973-8-6"),
                   email = "ph11@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Phil.jpg")),
                   confirmPassword = GetMD5("phil8611")
               },
               new GeneralUser
               {
                   userName = "kimmm",
                   name = "Kim",
                   password = GetMD5("kim7381m"),
                   dateOfBirth = DateTime.Parse("1973-8-1"),
                   email = "kimmm@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Kim.jpg")),
                   confirmPassword = GetMD5("kim7381m")
               },
               new GeneralUser
               {
                   userName = "st3v3n",
                   name = "Steven",
                   password = GetMD5("steve4646"),
                   dateOfBirth = DateTime.Parse("1946-4-1"),
                   email = "st3v3n@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Steven.jpg")),
                   confirmPassword = GetMD5("steve4646")
               },
               new GeneralUser
               {
                   userName = "dar133n",
                   name = "Darleen",
                   password = GetMD5("n331darr1"),
                   dateOfBirth = DateTime.Parse("1970-12-29"),
                   email = "dar133n@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Darleen.jpg")),
                   confirmPassword = GetMD5("n331darr1")
               },
               new GeneralUser
               {
                   userName = "t0ny0",
                   name = "Tony",
                   password = GetMD5("0tony04146"),
                   dateOfBirth = DateTime.Parse("1978-10-25"),
                   email = "t0ny@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Tony.jpg")),
                   confirmPassword = GetMD5("0tony04146")
               },
               new GeneralUser
               {
                   userName = "merrymary",
                   name = "Mary",
                   password = GetMD5("21m21e21r21y"),
                   dateOfBirth = DateTime.Parse("1950-7-21"),
                   email = "merrymary@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/user_images/Mary.jpg")),
                   confirmPassword = GetMD5("21m21e21r21y")
               },
               new GeneralUser
               {
                   userName = "amykhor",
                   name = "Amy Khor",
                   password = GetMD5("doctor123"),
                   dateOfBirth = DateTime.Parse("1988-5-22"),
                   email = "amykhor11@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/amy_Khor.jpg")),
                   confirmPassword = GetMD5("doctor123")
               },
               new GeneralUser
               {
                   userName = "ddandree",
                   name = "DeAndre",
                   password = GetMD5("doctor321"),
                   dateOfBirth = DateTime.Parse("1965-12-1"),
                   email = "ddandree@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/DeAndre.jpg")),
                   confirmPassword = GetMD5("doctor321")
               },
               new GeneralUser
               {
                   userName = "amotihar",
                   name = "AnJana Motihar",
                   password = GetMD5("doctor321"),
                   dateOfBirth = DateTime.Parse("1950-6-22"),
                   email = "amotihar@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/AnJana_Motihar.jpg")),
                   confirmPassword = GetMD5("doctor321")
               },
               new GeneralUser
               {
                   userName = "kcheesoo",
                   name = "Khee Chee Soo",
                   password = GetMD5("123doctor"),
                   dateOfBirth = DateTime.Parse("1960-12-1"),
                   email = "kcheesoo@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Khee_Chee_Soo.jpg")),
                   confirmPassword = GetMD5("123doctor")
               },
               new GeneralUser
               {
                   userName = "tanmhan1",
                   name = "Tan Min Han",
                   password = GetMD5("123doctor"),
                   dateOfBirth = DateTime.Parse("1966-4-5"),
                   email = "tanmhan1@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Tan_Min_Han.jpg")),
                   confirmPassword = GetMD5("123doctor")
               },
               new GeneralUser
               {
                   userName = "wilmhuang",
                   name = "William Huang",
                   password = GetMD5("123doctor312"),
                   dateOfBirth = DateTime.Parse("1980-10-15"),
                   email = "wilmhuang@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/William_Huang.jpg")),
                   confirmPassword = GetMD5("123doctor312")
               },
               new GeneralUser
               {
                   userName = "acacia_s",
                   name = "Acacia Soh",
                   password = GetMD5("123doctor11"),
                   dateOfBirth = DateTime.Parse("1975-3-1"),
                   email = "acacia_s@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Acacia_Soh.jpg")),
                   confirmPassword = GetMD5("123doctor11")
               },
               new GeneralUser
               {
                   userName = "herman_l",
                   name = "Herman",
                   password = GetMD5("123doctor321"),
                   dateOfBirth = DateTime.Parse("1955-11-8"),
                   email = "herman_l@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Herman.jpg")),
                   confirmPassword = GetMD5("123doctor321")
               },
               new GeneralUser
               {
                   userName = "ttzlee",
                   name = "Tan Tze Lee",
                   password = GetMD5("ttzlee111"),
                   dateOfBirth = DateTime.Parse("1970-2-12"),
                   email = "ttzlee@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Tan_Tze_Lee.jpg")),
                   confirmPassword = GetMD5("ttzlee111")
               },
               new GeneralUser
               {
                   userName = "shchan",
                   name = "Shin Chan",
                   password = GetMD5("inin123123"),
                   dateOfBirth = DateTime.Parse("1985-7-12"),
                   email = "shchan@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Shin_Chan.jpg")),
                   confirmPassword = GetMD5("inin123123")
               },
               new GeneralUser
               {
                   userName = "felchua",
                   name = "Felice Chua",
                   password = GetMD5("felfel1291"),
                   dateOfBirth = DateTime.Parse("1989-9-1"),
                   email = "felchua@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Felice_Chua.jpg")),
                   confirmPassword = GetMD5("felfel1291")
               },
               new GeneralUser
               {
                   userName = "gohalice",
                   name = "Alice Goh",
                   password = GetMD5("ggaa12112"),
                   dateOfBirth = DateTime.Parse("1990-1-9"),
                   email = "gohalice@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Alice_Goh.jpg")),
                   confirmPassword = GetMD5("ggaa12112")
               },
               new GeneralUser
               {
                   userName = "agastht",
                   name = "T.Agasthian",
                   password = GetMD5("agdoc11133"),
                   dateOfBirth = DateTime.Parse("1968-6-8"),
                   email = "agastht@gmail.com",
                   gender = "Male",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/T_Agasthian.jpg")),
                   confirmPassword = GetMD5("agdoc11133")
               },
               new GeneralUser
               {
                   userName = "chyeelee",
                   name = "Cheah Yee Lee",
                   password = GetMD5("leelee1919"),
                   dateOfBirth = DateTime.Parse("1990-1-9"),
                   email = "chyeelee@gmail.com",
                   gender = "Female",
                   profilePicture = File.ReadAllBytes(getImagePath("~/images/doctor_images/Cheah_Yee_Lee.jpg")),
                   confirmPassword = GetMD5("leelee1919")
               }
               );

            context.InterestedParty.AddOrUpdate(
                p => p.userName,
                new InterestedParty
                {
                    userName = "edna12",
                    smokerOrNot = false,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "mm233",
                    smokerOrNot = true,
                    bio = "I've been smoking for 40 years, but recently I'm trying to quit. Sharing my experiences on what works and does not!"
                },
                new InterestedParty
                {
                    userName = "royyy13",
                    smokerOrNot = false,
                    bio = "I am a retired doctor giving brief suggestions."
                },
                new InterestedParty
                {
                    userName = "sssusan11",
                    smokerOrNot = false,
                    bio = "I was a smoker for 40 years. I've quit for 26 years."
                },
                new InterestedParty
                {
                    userName = "jj665",
                    smokerOrNot = false,
                    bio = "Just trying to encourage my fellow smokers to quit."
                },
                new InterestedParty
                {
                    userName = "pete99",
                    smokerOrNot = false,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "jamesbond",
                    smokerOrNot = false,
                    bio = "Never too late to quit!"
                },
                new InterestedParty
                {
                    userName = "dddd01",
                    smokerOrNot = true,
                    bio = "Actively trying to quit smoking right now"
                },
                new InterestedParty
                {
                    userName = "lis45",
                    smokerOrNot = false,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "Mike9",
                    smokerOrNot = false,
                    bio = "Smoked for 45 years, if I could quit, you can too!"
                },
                new InterestedParty
                {
                    userName = "Jayyy13",
                    smokerOrNot = false,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "jo6na",
                    smokerOrNot = true,
                    bio = "Still in my journey to quitting! 6 months clean!"
                },
                new InterestedParty
                {
                    userName = "r4h4m",
                    smokerOrNot = false,
                    bio = "Sharing experiences of qutting along with my closed ones' experiences, hoping to encourage more to quit."
                },
                new InterestedParty
                {
                    userName = "dan77",
                    smokerOrNot = true,
                    bio = "Just started my journey to being smoke free. Hope all goes well"
                },
                new InterestedParty
                {
                    userName = "ph111",
                    smokerOrNot = false,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "kimmm",
                    smokerOrNot = true,
                    bio = "Been on a one year streak of being smoke free!"
                },
                new InterestedParty
                {
                    userName = "st3v3n",
                    smokerOrNot = true,
                    bio = ""
                },
                new InterestedParty
                {
                    userName = "dar133n",
                    smokerOrNot = true,
                    bio = "Trying to quit for my family."
                },
                new InterestedParty
                {
                    userName = "t0ny0",
                    smokerOrNot = true,
                    bio = "6 months streak!!!"
                },
                new InterestedParty
                {
                    userName = "merrymary",
                    smokerOrNot = true,
                    bio = "On a 2 years streak!"
                }

                );

            context.Doctor.AddOrUpdate(
                p => p.userName,
                new Doctor
                {
                    userName = "amykhor",
                    workLocation = "Ministry of Health",
                    description = "Specializing in Lung health",
                    contactNo = 98765432,
                    doctorID = "D0001B",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "ddandree",
                    workLocation = "Centers for Disease Control and Prevention",
                    description = "Research on Smoking Causes",
                    contactNo = 96357891,
                    doctorID = "D0002C",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "amotihar",
                    workLocation = "National Cancer Centre Singapore",
                    description = "Specializing in Lung Cancer",
                    contactNo = 96542873,
                    doctorID = "M0003A",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "kcheesoo",
                    workLocation = "National Cancer Centre",
                    description = "Specializing in Head and Neck Cancer",
                    contactNo = 97391692,
                    doctorID = "D0004G",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "tanmhan1",
                    workLocation = "NUS School of Public Health ",
                    description = "Research on Smoking and its cons",
                    contactNo = 91118222,
                    doctorID = "MP0105H",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "wilmhuang",
                    workLocation = "Gleneagles Hospital",
                    description = "Specializing in Cancer Cure",
                    contactNo = 97878787,
                    doctorID = "MB0036R",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "acacia_s",
                    workLocation = "Dover Hospice",
                    description = "Specializing in Social Services",
                    contactNo = 88881818,
                    doctorID = "DA0023B",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "herman_l",
                    workLocation = "Cancer Connect",
                    description = "Specializing in Cancer",
                    contactNo = 91618282,
                    doctorID = "DP0008Z",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "ttzlee",
                    workLocation = "Chronic Obstructive Pulmonary Disease Association",
                    description = "Specializing in lung related diseases",
                    contactNo = 93337333,
                    doctorID = "NZ0009E",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "shchan",
                    workLocation = "Health Hub",
                    description = "Specializing in Dietary needs",
                    contactNo = 97648888,
                    doctorID = "DE0010E",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "felchua",
                    workLocation = "Ministry of Health",
                    description = "Specializing in Smoking and Cancer Research",
                    contactNo = 95722884,
                    doctorID = "UT1311T",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "gohalice",
                    workLocation = "Ministry of Health",
                    description = "Specializing in Nicotine Research",
                    contactNo = 91648764,
                    doctorID = "DQ5501T",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "agastht",
                    workLocation = "Mount Elizabeth Medical Centre",
                    description = "Specializing in heart diseases",
                    contactNo = 97668422,
                    doctorID = "AD9213J",
                    adminVerify = true
                },
                new Doctor
                {
                    userName = "chyeelee",
                    workLocation = "Gleneagles Hospital",
                    description = "Specializing in tumour removal",
                    contactNo = 87889828,
                    doctorID = "ED6614O",
                    adminVerify = true
                }
                );

            /*context.Article.AddOrUpdate(
                p => p.userName,
                new Article
                {
                    userName = "doctor1",
                    title = "Dummy Title1",
                    body = "Dummy body 1",
                    articleStatus = "pending",
                    articlePicture = File.ReadAllBytes(getImagePath("~/images/dummy_article.jpg")),
                    postDate = DateTime.Parse("2020-10-11")

                },
                new Article
                {
                    userName = "doctor1",
                    title = "Dummy Title2",
                    body = "Dummy body 2",
                    articleStatus = "approved",
                    articlePicture = File.ReadAllBytes(getImagePath("~/images/dummy_article.jpg")),
                    postDate = DateTime.Parse("2020-10-11")
                },
                new Article
                {
                    userName = "doctor2",
                    title = "Dummy Title3",
                    body = "Dummy body 3",
                    articleStatus = "pending",
                    articlePicture = File.ReadAllBytes(getImagePath("~/images/dummy_article.jpg")),
                    postDate = DateTime.Parse("2020-10-11")
                },
                new Article
                {
                    userName = "doctor2",
                    title = "Dummy Title4",
                    body = "Dummy body 4",
                    articleStatus = "pending",
                    articlePicture = File.ReadAllBytes(getImagePath("~/images/dummy_article.jpg")),
                    postDate = DateTime.Parse("2020-10-11")
                }

                );*/





        }
    }
}
