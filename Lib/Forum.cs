using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IntellidateLib
{
    public class Forum
    {
        /// <summary>
        /// The forum post ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; } 
        
        public int PostRefID { get; set; }

        /// <summary>
        /// The admin id posting the item
        /// </summary>
        public int AdminRefId { get; set; }
        public Admin Admin
        {
            get
            {
                return new Admin().GetAdminDetails(this.AdminRefId);
            }
        }

        /// <summary>
        /// The category of the post
        /// </summary>
        public int ForumCategoryID { get; set; }
        public ForumCategory ForumCategory
        {
            get
            {
                return new ForumCategory().GetForumCategory(this.ForumCategoryID);
            }
        }

        /// <summary>
        /// The id of the immediate parent. If null, the post just started
        /// </summary>
        public int ParentPostID { get; set; }
        public Forum ParentPost
        {
            get
            {
                if (this.ParentPostID != 0)
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The title of the post
        /// </summary>
        public string PostTitle { get; set; }

        /// <summary>
        /// The contents of the post
        /// </summary>
        public string PostText { get; set; }

        /// <summary>
        /// The timstamp of the post made
        /// </summary>
        public DateTime PostTimeStamp { get; set; }
        public DateTime LastActionTimeStamp { get; set; }

        /// <summary>
        /// Status of the post
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The attachments if any with the post
        /// </summary>
        public ForumAttachment[] ForumAttachments { get; set; }

        public Forum[] PostChildren {
            get
            {
                if (this.PostRefID != 0)
                {
                    return GetPostChildren(this.PostRefID);
                }
                else
                {
                    return null;
                }
                
            }
        }

        public Forum AddNewPost(int adminId, int categoryId, string postTitle, string postContent, string[] attachments)
        {
            try
            {

                
                int m_PostRefID = 0;

                DateTime m_TimeStamp = DateTime.Now;

                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        //
                        in_forumpost_trn newPost = new in_forumpost_trn
                        {
                            adminid = adminId,
                            posttitle = postTitle,
                            posttext = postContent,
                            status = Constants.activeStatus,
                            timestamp = m_TimeStamp,
                            categoryid = categoryId
                        };
                        mainDB.in_forumpost_trn.Add(newPost);
                        mainDB.SaveChanges();
                        m_PostRefID = newPost.trnid;

                        if (attachments.Length > 0)
                        {
                            foreach (string m_EachAttachment in attachments)
                            {
                                in_attachment_mst attachmentObject = new in_attachment_mst
                                {
                                    AttachmentPath = m_EachAttachment,
                                    FolderNaame = m_EachAttachment
                                };
                                mainDB.in_attachment_mst.Add(attachmentObject);
                                mainDB.SaveChanges();
                            }
                        }


                        MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                        var m_Collection = cachingDataBase.GetCollection<Forum>(Constants.forumClass);
                        var m_Query = Query<Forum>.Where(x => x.PostRefID == m_PostRefID);
                        Forum newCachingPost = new Forum
                        {
                            ForumCategoryID = categoryId,
                            PostTitle = postTitle,
                            PostText = postContent,
                            PostTimeStamp = m_TimeStamp,
                            PostRefID = m_PostRefID,
                            LastActionTimeStamp = m_TimeStamp,
                            Status = Constants.activeStatus,
                            AdminRefId = adminId,
                            ParentPostID = 0
                        };
                        m_Collection.Insert(newCachingPost);
                        var m_Return = m_Collection.FindOne(m_Query);
                        transaction.Complete();

                        return (Forum)m_Return;
                    }
                };
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Forum", "AddNewPost");
                return null;
            }
        }

        public Forum ReplyPost(int adminId, int parentPostId, string postContent)
        {
            try
            {
                int m_PostRefId = 0;
                
                Forum parentPostDetails = GetPostDetails(parentPostId);
                int m_CategoryId = new ForumCategory().GetForumRefID(parentPostDetails.ForumCategoryID.ToString());
                
                DateTime m_TimeStamp = DateTime.Now;

                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {

                        //
                        in_forumpost_trn newPost = new in_forumpost_trn
                        {
                            adminid = adminId,
                            posttitle = "",
                            posttext = postContent,
                            status = Constants.activeStatus,
                            timestamp = m_TimeStamp,
                            parentpostid = parentPostDetails.PostRefID,
                            categoryid = m_CategoryId

                        };
                        mainDB.in_forumpost_trn.Add(newPost);
                        mainDB.SaveChanges();
                        m_PostRefId = newPost.trnid;
                        try
                        {
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<Forum>(Constants.forumClass);
                            var m_Query = Query<Forum>.Where(x => x.PostRefID == m_PostRefId);
                            Forum newCachingPost = new Forum
                            {
                                ForumCategoryID = parentPostDetails.ForumCategoryID,
                                PostTitle = PostTitle,
                                ParentPostID = parentPostDetails.PostRefID,
                                PostText = postContent,
                                PostTimeStamp = m_TimeStamp,
                                PostRefID = m_PostRefId,
                                LastActionTimeStamp = m_TimeStamp,
                                AdminRefId = adminId
                            };
                            m_Collection.Insert(newCachingPost);
                            var m_Return = m_Collection.FindOne(m_Query);

                            // Updating the last update time of the parent post
                            var m_Update = Update<Forum>.Set(x => x.LastActionTimeStamp, DateTime.Now);
                            var m_UpdateSearchQuery = Query<Forum>.EQ(x => x.ParentPostID, parentPostDetails.PostRefID);
                            m_Collection.Update(m_UpdateSearchQuery, m_Update);
                            transaction.Complete();
                            return (Forum)m_Return;
                        }
                        catch (Exception exception)
                        { 
                            new Error().LogError(exception, "Forum", "ReplyPost");
                            return null;
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Forum", "ReplyPost");
                return null;
            }
        }

        public Forum[] GetMainPosts(int lastShownId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //var _ForumCategoryCollection = _CachingDatabase.GetCollection<ForumCategory>("ForumCategory");
                //var _adminCategoryQuery = Query<ForumCategory>.EQ(x=>x.)

                var m_Collection = cachingDataBase.GetCollection<Forum>(Constants.forumClass);
                var m_Forums = m_Collection.FindAll().ToList();

                m_Forums = m_Forums.Where(x => x.ParentPostID == 0).ToList();
                m_Forums = m_Forums.OrderByDescending(x => x.LastActionTimeStamp).ToList();
                lastShownId = (lastShownId == 0) ? lastShownId = Int32.MaxValue : lastShownId;
                m_Forums = m_Forums.Where(x => x.PostRefID < lastShownId).Take(20).ToList();

                return (Forum[])m_Forums.ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Forum", "GetMainPosts");
                return null;
            }
        }

        public Forum[] GetPostChildren(int postId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Collection = cachingDataBase.GetCollection<Forum>(Constants.forumClass);
                var m_Query = Query<Forum>.Where(x => x.ParentPostID == postId);
                var m_Forums = m_Collection.Find(m_Query).ToArray();
                return (Forum[])m_Forums;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Forum", "GetPostChildren");
                return null;
            }
        }

        public Forum GetPostDetails(int postId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Collection = cachingDataBase.GetCollection<Forum>(Constants.forumClass);
                var m_Query = Query<Forum>.Where(x => x.PostRefID == postId);
                var m_Forums = m_Collection.FindOne(m_Query);
                return (Forum)m_Forums;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Forum", "GetPostDetails");
                return null;
            }
        }
    }
}
