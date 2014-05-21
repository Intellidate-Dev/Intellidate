using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public class ForumNotification
    {
        public ObjectId _id { get; set; }

        public ObjectId ForumPostID { get; set; }
        public Forum ForumPost { get; set; }

        public DateTime TimeStamp { get; set; }
        
        public ObjectId SenderAdminID { get; set; }
        public Admin SenderAdmin { get; set; }

        public ObjectId RecieverAdminID { get; set; }
        public Admin RecieverAdmin { get; set; }

        public string[] GetAffectedAdminIDs(string forumPostId)
        {
            try
            {
                // If the post is parent post.
                return null;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "ForumNotification", "GetAffectedAdminIDs");
                return null;
            }
        }

        public ForumNotification[] AddForumNotification(string senderAdminId, string forumPostId)
        {
            try
            {
                // Get the possible affected admins for this particular forum post
                return null;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "ForumNotification", "AddForumNotification");
                return null;
            }
        }
    }
}
