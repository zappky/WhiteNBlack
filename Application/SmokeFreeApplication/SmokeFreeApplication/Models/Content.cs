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
        public UserEntry owner { get; set; }
        //date of being posted
        public DateTime postDate { get; set; }
        //just contain list of url links of pictures to be used in the content
        List<string> pictures { get; set; }

        public string texts { get; set; }

        public int id { get; set; }

        public string Title { get; set; }

        public bool DeleteContent()
        {
            //maybe do a if else to check for the user type first?
            texts = "--Content has been deleted--";
            pictures.Clear();
            //Leave the Owner and post data intact

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
    public class ContentManager<T> where T: class
    {
        public List<T> contentsList { get; set; }

        public ContentManager() { }

        public bool DeleteContent(Content aContent)
        {
            return aContent.DeleteContent();
        }


    }
}