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
        public int userName { get; set; }

        //List<string> Pictures { get; set; }

        public string title { get; set; }
        public string body { get; set; }
        public DateTime postDate { get; set; }

        public bool DeleteContent()
        {
           

            return true;
        }

    }


    //Contain list of articles or story
    //contain method to manage story/article
    //such as delete, insert, edit, share? 
    //Maybe not be needed as we have database which we can directly manupliate
    //Not familar with website coding enough to be certain
    //instantiate this class like
    //ContentManager<Story> a =  new ContentManager<Story>();
    /*public class ContentManager<T> where T : class
    {
        public List<T> contentsList { get; set; }

        public ContentManager() { }

        public bool DeleteContent(Content aContent)
        {
            return aContent.DeleteContent();
        }


    }*/
}