using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Content
    {
        //the person who post the content, genericUser or UserEntry,depending which one survives.
        //From this object we can obatain user profile pic, name
        public int UserName { get; set; }

        //List<string> Pictures { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PostDate { get; set; }


    }


    //Contain list of articles or story
    //contain method to manage story/article
    //such as delete, insert, edit, share? 
    //Maybe not be needed as we have database which we can directly manupliate
    //Not familar with website coding enough to be certain
    //instantiate this class like
    //ContentManager<Story> a =  new ContentManager<Story>();
    public class ContentManager<T> where T: class
    {
        public List<T> contentsList { get; set; }

        public ContentManager() { }



    }
}