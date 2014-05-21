
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

namespace IntellidateLib
{
    public class ForumCategory
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        
        public int CategoryRefID { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfUsers { get; set; }


        public ForumCategory[] GetAllForumCategories()
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active users data from caching db 
                var m_Query = Query<ForumCategory>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                List<ForumCategory> forumCategories = new List<ForumCategory>();

                forumCategories =  m_Collection.Find(m_Query).ToList();


                return forumCategories.ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, Constants.getAllForumCategoriesMethod);
                return null;
            }
        }

        public int GetForumRefID(string categoryId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting user details from caching db 
                var m_Query = Query<ForumCategory>.EQ(x => x._id, categoryId);
                var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                ForumCategory forumCategory =  m_Collection.FindOne(m_Query);
                return forumCategory.CategoryRefID;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, "GetForumRefID");
                return 0;
            }
        }

        public ForumCategory GetForumCategory(int categoryId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting user details from caching db 
                var m_Query = Query<ForumCategory>.EQ(x => x.CategoryRefID, categoryId);
                var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                ForumCategory forumCategory = m_Collection.FindOne(m_Query);
                return forumCategory;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, "GetForumCategory");
                return null;
            }
        }

        public ForumCategory[] GetAdminForumCategories(int adminId)
        {
            try
            {
                string[] m_CategoryIDs;
                Admin adminDetails = new Admin().GetAdminDetails(adminId);
                m_CategoryIDs = adminDetails.ForumPrivileges;


                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting user details from caching db 
                var m_Query = Query<ForumCategory>.Where(x => m_CategoryIDs.Contains(x._id));
                var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                List<ForumCategory> lstForumCategory = new List<ForumCategory>();
                lstForumCategory = m_Collection.Find(m_Query).ToList();


                return lstForumCategory.ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, "GetForumCategories");
                return null;
            }
        }
        
        public ForumCategory AddNewCategory(string categoryName)
        {
            try
            {
                DateTime m_CreatedDate = DateTime.Now;
                int m_CategoryId = 0;

                // insert the album details in the Main DB and Cache DB
                //Insert category details in the Main DB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    in_forumscategory_mst forumCategoryObject = new in_forumscategory_mst
                    {
                        categoryname = categoryName,
                        status = Constants.activeStatus
                    };
                    mainDB.in_forumscategory_mst.Add(forumCategoryObject);
                    mainDB.SaveChanges();
                    m_CategoryId = forumCategoryObject.categoryid; 
                };

                ForumCategory cachingObject = new ForumCategory();

                cachingObject.CategoryRefID = m_CategoryId;
                cachingObject.CategoryName = categoryName;
                cachingObject.Status = Constants.activeStatus;
                cachingObject.NumberOfPosts = 0;
                cachingObject.NumberOfUsers = 0;
                
                if (m_CategoryId > 0)
                {
                    try
                    {
                        MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                        var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                        m_Collection.Save(cachingObject);
                    }
                    catch (Exception ex2)
                    {
                        new Error().LogError(ex2, "ForumCategory", "AddNewCategory");
                    }   
                }
                return cachingObject;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, Constants.addNewCategoryMethod);
                return null;
            }
        }

        public ForumCategory EditCategory(string categoryId, string categoryName)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                DateTime m_CreatedDate = DateTime.Now;
                int m_CategoryID = 0;

                var m_Query = Query<ForumCategory>.Where(x => x._id == categoryId);
                var m_CategoryDetails = m_Collection.FindOne(m_Query);
                m_CategoryID = m_CategoryDetails.CategoryRefID;
                var m_Update = Update<ForumCategory>.Set(x => x.CategoryName, categoryName);
                m_Collection.Update(m_Query, m_Update);

                // insert the album details in the Main DB and Cache DB
                //Insert category details in the Main DB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    var m_ExistingCatg = mainDB.in_forumscategory_mst.Where(x => x.categoryid == m_CategoryID).SingleOrDefault();
                    if (m_ExistingCatg != null)
                    {
                        m_ExistingCatg.categoryname = categoryName;
                        mainDB.SaveChanges();
                    }
                };
                m_CategoryDetails.CategoryName = categoryName;
                return m_CategoryDetails;
                
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, Constants.addNewCategoryMethod);
                return null;
            }
        }

        public ForumCategory[] GetAdminCategories(int adminId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "ForumCategory", "GetAdminCategories");
                return null;
            }
        }

      
        public bool DeleteCategory(string categoryId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                // change user password in MainDB and CachingDB
                var m_Query = Query<ForumCategory>.EQ(e => e._id, categoryId);
                var m_Collection = cachingDataBase.GetCollection<ForumCategory>(Constants.forumCategoryClass);
                var m_ExistingObject = m_Collection.FindOne(m_Query);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        in_forumscategory_mst forumCategoryObj = mainDB.in_forumscategory_mst.SingleOrDefault(c => c.categoryid == m_ExistingObject.CategoryRefID);
                        mainDB.in_forumscategory_mst.Remove(forumCategoryObj);
                        mainDB.SaveChanges();
                    }
                    //update CachingDB 
                    m_Collection.Remove(m_Query);
                }
                return true;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.forumCategoryClass, "DeleteCategory");
                return false;
            }
        }
    }
}
