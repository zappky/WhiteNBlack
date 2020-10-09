using SmokeFreeApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.EnterpriseServices;
using System.Linq;
using System.Web;


/// <summary>
/// Just builder class to build account classes
/// Motivation is to make construction account class to be easier/clearer
/// There is a problem of builder class being copied and pasted  from userBuilder
/// Inhertance doesnt work as the return type would be the base type and that prevents chaining of function call
/// tried using generic but cannot get it to work
/// Online solution says it is a convarient return problem not supported in C#
/// Oh well, most probably gonna be dump, since i maybe overstepping into other people work ¯\_(ツ)_/¯
/// </summary>

namespace SmokeFreeApplication.Models
{
    //personal information
    public class UserBio
    {
        public string name { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
    }
    

    //user meta information

    public class UserMeta
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string profilePicture { get; set; }

    }

    public class UserEntry
    {
        public UserBio bio { get; set; }
        public UserMeta meta { get; set; }
    }


    public class UserBuilder
    {
        private UserEntry userEntry = new UserEntry();
        public UserBuilder() { }

        public UserBuilder UserBio(UserBio bio)
        {
            userEntry.bio = bio;
            return this;
        }
        public  UserBuilder UserBio(string name, DateTime dob,string gender)
        {
            userEntry.bio = new UserBio() { name = name,dateOfBirth = dob, gender = gender };
            return this;
        }



        public  UserBuilder UserMeta(UserMeta meta)
        {
            userEntry.meta = meta;
            return this;
        }

        public  UserBuilder UserMeta(string userName, string email, string password, string profilePicture)
        {
            userEntry.meta = new UserMeta() { userName = userName, email = email, password = password, profilePicture = profilePicture };
            return this;
        }



        public UserEntry Build()
        {
            return userEntry;
        }
    }

    public class DocInfo
    {
        public string workLocation { get; set; }
        public string description { get; set; }
        public int contactNo { get; set; }
        public string doctorID { get; set; }
        public bool adminVerify { get; set; }
    }

    public class DocEntry:UserEntry
    {
        public DocInfo docInfo { get; set; }
    }

    public class DocBuilder
    {
        private DocEntry userEntry = new DocEntry();

        public DocBuilder() { }

        public DocBuilder UserBio(UserBio bio)
        {
            userEntry.bio = bio;
            return this;
        }
        public DocBuilder UserBio(string name, DateTime dob, string gender)
        {
            userEntry.bio = new UserBio() { name = name, dateOfBirth = dob, gender = gender };
            return this;
        }



        public DocBuilder UserMeta(UserMeta meta)
        {
            userEntry.meta = meta;
            return this;
        }

        public DocBuilder UserMeta(string userName, string email, string password, string profilePicture)
        {
            userEntry.meta = new UserMeta() { userName = userName, email = email, password = password, profilePicture = profilePicture };
            return this;
        }

        public DocBuilder DocInfo(DocInfo docInfo)
        {
            userEntry.docInfo = docInfo;
            return this;
        }

        public DocBuilder DocInfo(string workLocation, string description, int contactNo, string doctorID,bool adminVerify)
        {
            userEntry.docInfo = new DocInfo() { workLocation = workLocation, description = description, contactNo = contactNo, doctorID = doctorID , adminVerify = adminVerify };
            return this;

        }

        public DocEntry Build()
        {
            return userEntry;
        }

    }
}

public class InterestedPartyInfo
{
    public bool smokerOrNot { get; set; }
    public string personalBio { get; set; }
}

public class InterestedPartyEntry : UserEntry
{
    public InterestedPartyInfo interestedPartyInfo { get; set; }
}

public class InterestedPartyBuilder
{
    private InterestedPartyEntry userEntry = new InterestedPartyEntry();

    public InterestedPartyBuilder() { }

    public InterestedPartyBuilder UserBio(UserBio bio)
    {
        userEntry.bio = bio;
        return this;
    }
    public InterestedPartyBuilder UserBio(string name, DateTime dob, string gender)
    {
        userEntry.bio = new UserBio() { name = name, dateOfBirth = dob, gender = gender };
        return this;
    }



    public InterestedPartyBuilder UserMeta(UserMeta meta)
    {
        userEntry.meta = meta;
        return this;
    }

    public InterestedPartyBuilder UserMeta(string userName, string email, string password, string profilePicture)
    {
        userEntry.meta = new UserMeta() { userName = userName, email = email, password = password, profilePicture = profilePicture };
        return this;
    }


    public InterestedPartyBuilder InterestedPartyInfo(InterestedPartyInfo interestedPartyInfo)
    {
        userEntry.interestedPartyInfo = interestedPartyInfo;
        return this;
    }

    public InterestedPartyBuilder InterestedPartyInfo(bool smokerOrNot, string personalBio)
    {
        userEntry.interestedPartyInfo = new InterestedPartyInfo() { smokerOrNot = smokerOrNot, personalBio = personalBio };
        return this;
    }


    public InterestedPartyEntry Build()
    {
        return userEntry;
    }

}
public class AdminBuilder
{
    private Admin userEntry = new Admin();

    public AdminBuilder() { }

    public AdminBuilder UserBio(UserBio bio)
    {
        //userEntry.bio = bio;
        return this;
    }
    public AdminBuilder UserBio(string name, DateTime dob, string gender)
    {
        //userEntry.bio = new UserBio() { name = name, dateOfBirth = dob, gender = gender };
        return this;
    }



    public AdminBuilder UserMeta(UserMeta meta)
    {
        //userEntry.meta = meta;
        return this;
    }

    public AdminBuilder UserMeta(string userName, string email, string password, string profilePicture)
    {
        //userEntry.meta = new UserMeta() { userName = userName, email = email, password = password, profilePicture = profilePicture };
        return this;
    }

    public AdminBuilder AdminInfo(string id)
    {
        userEntry.id = id;
        return this;
    }

    public Admin Build()
    {
        return userEntry;
    }
}




