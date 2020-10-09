using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Comment
    {
        //the user who posted it, can be UserEntry or genericUser, depends on which one surives
        // We can obtain user profile pic and user name here
        public UserEntry user;

        //the content of the post itself
        public string content;

    }

    //Contain list of comments
    //contain method to manage comment, hence the comment system itself
    //such as delete, insert, edit, share? 
    //Maybe not be needed as we have database which we can directly manupliate
    //Not familar with website coding enough to be certain
    public class CommentManager
    {
        List<Comment> comments;

        //just place holder for now
        public void PostComment() { }
        public void ReplyComment() { }
    }
}