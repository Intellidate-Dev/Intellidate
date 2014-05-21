using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
    /// <summary>
    /// The Photo class defines all the generic properties of the user's photos
    /// </summary>
    public class Photo
    {

        /// <summary>
        /// The Photo identifier  
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }


        /// <summary>
        /// The photo ID from the collection or MySQL Database if any..dsfsdf
        /// </summary>
        public int PhotoId { get; set; }

        // <summary>
        /// The photo Title from the collection or MySQL Database if any..
        /// </summary>
        public string PhotoTitle { get; set; }

        /// <summary>
        /// The Album ID from the collection or MySQL Database if any..
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// The GetAdminPhotoReport from AdminPhoto collection 
        /// </summary>
        public AdminPhoto GetAdminPhotoReport
        {
            get { return new AdminPhoto().GetAdminPhotoReportById(AlbumId); }
        }

        /// <summary>
        /// The User ID from the collection or MySQL Database if any..
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The AttachmentId where photos  collection
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// The GetAttachments from Attachments collection
        /// </summary>
        public Attachments GetAttachments
        {
            get { return new Attachments().GetAttachmentById(AttachmentId); }
        }

        /// <summary>
        /// The Is User Default Photo from the collection or MySQL Database if any..
        /// </summary>
        public bool IsUserDefault { get; set; }

        /// <summary>
        /// The Is Album Default Photo from the collection or MySQL Database if any..
        /// </summary>
        public bool IsAlbumDefault { get; set; }

        /// <summary>
        /// The Created Date from the collection or MySQL Database if any..
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The Status from the collection or MySQL Database if any..
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// The Size of Photo Uploaded from the collection or MySQL Database if any..
        /// </summary>
        public decimal PhotoSize { get; set; }

        public AbusiveReport[] GetAbusiveReport 
        {
            get { return new AbusiveReport().GetAbusiveReport(PhotoId); }
        }




        ///<summary>
        /// Save Photo method can be saves Photo collection in caching db and main db
        /// </summary>
        /// <param name="albumId">The Master Id of album collection</param>
        /// <param name="userId">The user indentity fileld from user master collection</param>
        /// <param name="path">The photo location url or path name </param>
        /// <param name="isAlbumDefault">Album default photo setting </param>
        /// <param name="isUserDefault">User Profile default photo setting</param>
        public bool SavePhoto(int albumId, int userId, string path, string folderName, bool isUserDefault, bool isAlbumDefault, decimal photoSize)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_newCollection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                DateTime m_CreatedDate = DateTime.Now;
                int m_PhotoID = 0;
                Attachments m_NewAttObj = new Attachments().AddAttachment(folderName, path);
                int m_AttId = m_NewAttObj._RefID;
                // insert the photo details in the Main DB and Cache DB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {

                    using (var transaction = new TransactionScope())
                    {
                        in_photo_mst mstPhotoObject = new in_photo_mst
                        {
                            UserID = userId,
                            albumID = albumId,
                            AttachmentId = m_AttId,
                            IsDefaultUserPhoto = isUserDefault,
                            IsDefaultAlbumPhoto = isAlbumDefault,
                            CreatedDate = DateTime.Now.ToUniversalTime(),
                            PhotoSize = photoSize,
                            Status = 0
                        };
                        mainDB.in_photo_mst.Add(mstPhotoObject);
                        mainDB.SaveChanges();
                        m_PhotoID = mstPhotoObject.photoID;

                        try
                        {
                            // Once the insert is done in the MainDB, do the insert on the Caching DB
                            Photo cachingObject = new Photo();
                            cachingObject.PhotoId = m_PhotoID;
                            cachingObject.UserId = userId;
                            cachingObject.AlbumId = albumId;
                            cachingObject.AttachmentId = m_AttId;
                            cachingObject.IsUserDefault = isUserDefault;
                            cachingObject.IsAlbumDefault = isAlbumDefault;
                            cachingObject.CreatedDate = DateTime.Now.ToUniversalTime();
                            cachingObject.PhotoSize = photoSize;
                            cachingObject.Status = 0;
                            m_newCollection.Save(cachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.photoClass, Constants.savePhotoMethod);
                            return false;
                        }
                      
                    }
                };
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.savePhotoMethod);
                return false;
            }
        }




        /// <summary>
        /// The DeletePhotos method can change the status to R (Inactive)
        /// </summary>
        public bool DeletePhotos(int photoId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the user we just updates the status of user A(Active) to I(Inactive)
                var m_SelectQuery = Query<Photo>.EQ(e => e.PhotoId, photoId);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_photo_mst mstPhotoObj = mainDB.in_photo_mst.SingleOrDefault(c => c.photoID == photoId);
                            mstPhotoObj.Status = 0;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Photo cachingObject = new Photo();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.photoId, photoId));
                                var m_Update = Update.Set(Constants.status, 0);
                                var m_SortBy = SortBy.Descending(Constants.photoId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.photoClass, Constants.deletePhotosMethod);
                                return false;
                            }
                            
                        }
                    };
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.deletePhotosMethod);
                return false;
            }
        }




        /// <summary>
        /// The GetPhotos method gets the single album photos based on album id
        /// </summary>
        public Photo[] GetPhotos(int albumId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active photo data from caching db
                var m_Query = Query<Photo>.Where(e => e.Status == 2 & e.AlbumId == albumId);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getPhotosMethod);
                return null;
            }
        }




        /// <summary>
        /// The GetAlbumPhoto method gets the default album photo based on album id
        /// </summary>
        public Photo GetAlbumPhoto(int albumId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting album default Album photo details from caching db
                var m_Query = Query<Photo>.Where(e => e.Status ==2 & e.AlbumId == albumId & e.IsAlbumDefault == true);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getAlbumPhotoMethod);
                return null;
            }
        }




        /// <summary>
        /// The GetProfilePhoto method gets the default profile photo of user based on user id
        /// </summary>
        public Photo GetProfilePhoto(int userId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting user's profile default photo details from caching db
                var m_Query = Query<Photo>.Where(e => e.Status == 2 & e.UserId == userId & e.IsUserDefault == true);
                var m_Collection = CachingDatabase.GetCollection<Photo>(Constants.photoClass);
                var m_x = m_Collection.FindOne(m_Query);
                return m_x;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getProfilePhotoMethod);
                return null;
            }
        }
        
        
        
        
        /// <summary>
        /// The GetAllPhotos method gets the all the photos uploaded from user based on user id
        /// </summary>
        public Photo[] GetAllPhotos(int userId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting user's profile default photo details from caching db
                var m_Query = Query<Photo>.Where(e => e.Status == 2 & e.UserId == userId);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_Collection.Find(m_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getAllPhotosMethod);
                return null;
            }
        }




        /// <summary>
        /// The GetUserUsedSpace method total memory occupied for photos by single user
        /// </summary>
        public decimal GetUserUsedSpaceForPhotos(int userId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //query for photo collection
                var m_Query = Query<Photo>.Where(e => e.Status == 2 & e.UserId == userId);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                //to return the sum of the photo sizes for the selected user
                return m_Collection.Find(m_Query).Sum(c => c.PhotoSize);
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getUserUsedSpaceForPhotosMethod);
                return 0;
            }
        }




        /// <summary>
        /// get photos based on status for new requested=0,Reported(Pending)=1,Rejected=-1,Approved=2
        /// </summary>
        public Photo[] GetPhotosBasedOnStatus(int status)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active photo data from caching db
                var m_Query = Query<Photo>.Where(e => e.Status == status);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getPhotosBasedOnStatusMethod);
                return null;
            }
        }




        public Photo ApproveOrRejectPhoto(bool status,int photoId,int adminId,string comment)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the user we just updates the status of user A(Active) to I(Inactive)
                var m_SelectQuery = Query<Photo>.Where(e=>e.PhotoId==photoId);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                Photo existingObject = m_Collection.FindOne(m_SelectQuery);
                Photo cachingObject = new Photo();
                // If existing already in the database  Update MainDb return the existing object
                    new AdminPhoto().AddAdminPhotoReport(adminId, photoId, comment, status);

                    if (status == true && existingObject.Status == 0 || status == true && existingObject.Status == 1)
                    {
                        using (intellidatev2Entities mainDB = new intellidatev2Entities())
                        {
                            using (var transaction = new TransactionScope())
                            {
                                in_photo_mst _PhotoObj = mainDB.in_photo_mst.SingleOrDefault(c => c.photoID == photoId);
                                _PhotoObj.Status = existingObject.Status + 1;
                                mainDB.SaveChanges();

                                //Update CachingDB 
                                try
                                {
                                    var m_UpdateQuery = Query.And(Query<Photo>.EQ(x => x.PhotoId, photoId));
                                    var m_Update = Update<Photo>.Set(x => x.Status, existingObject.Status + 1);
                                    var m_sortBy = SortBy.Descending(Constants.photoId);
                                    var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_sortBy, m_Update, true);
                                    cachingObject = BsonSerializer.Deserialize<Photo>(m_Result.ModifiedDocument);
                                    transaction.Complete();
                                }
                                catch (Exception exception)
                                {
                                    new Error().LogError(exception, Constants.photoClass, Constants.approveOrRejectPhotoMethod);
                                    return null;
                                }
                                
                            }
                        }
                        
                    }
                    if (status == false && existingObject.Status == 0 || status == false && existingObject.Status == 1) 
                    {
                        using (intellidatev2Entities mainDB = new intellidatev2Entities())
                        {
                            using (var transaction = new TransactionScope())
                            {
                                in_photo_mst mstPhotoObj = mainDB.in_photo_mst.SingleOrDefault(c => c.photoID == photoId);
                                mstPhotoObj.Status = -1;
                                mainDB.SaveChanges();

                                try
                                {
                                    //Update CachingDB 

                                    var m_UpdateQuery = Query.And(Query.EQ(Constants.photoId, photoId));
                                    var m_Update = Update.Set(Constants.status, -1);
                                    var m_sortBy = SortBy.Descending(Constants.photoId);
                                    var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_sortBy, m_Update, true);
                                    cachingObject = BsonSerializer.Deserialize<Photo>(m_Result.ModifiedDocument);
                                    transaction.Complete();
                                }
                                catch (Exception exception)
                                {
                                    new Error().LogError(exception, Constants.photoClass, Constants.approveOrRejectPhotoMethod);
                                    return null;
                                }
                               
                            }
                        }
                    }


                    if (status == true && existingObject.Status == -1)
                    {
                        using (intellidatev2Entities mainDB = new intellidatev2Entities())
                        {
                            using (var transaction = new TransactionScope())
                            {
                                in_photo_mst mstPhotoObj = mainDB.in_photo_mst.SingleOrDefault(c => c.photoID == photoId);
                                mstPhotoObj.Status = existingObject.Status + 2;
                                mainDB.SaveChanges();
                                try
                                {
                                    //Update CachingDB 

                                    var m_UpdateQuery = Query.And(Query.EQ(Constants.photoId, photoId));
                                    var m_Update = Update.Set(Constants.status, existingObject.Status + 2);
                                    var m_SortBy = SortBy.Descending(Constants.photoId);
                                    var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                    cachingObject = BsonSerializer.Deserialize<Photo>(m_Result.ModifiedDocument);
                                    transaction.Complete();
                                }
                                catch (Exception exception)
                                {
                                    new Error().LogError(exception, Constants.photoClass, Constants.approveOrRejectPhotoMethod);
                                    return null;
                                }
                               
                            }
                        }
                    }
                    if (status == false && existingObject.Status == 2)
                    {
                        using (intellidatev2Entities mainDB = new intellidatev2Entities())
                        {
                            using (var transaction = new TransactionScope())
                            {

                                in_photo_mst mstPhotoObj = mainDB.in_photo_mst.SingleOrDefault(c => c.photoID == photoId);
                                mstPhotoObj.Status = -1;
                                mainDB.SaveChanges();

                                try
                                {
                                    //Update CachingDB 

                                    var m_UpdateQuery = Query.And(Query.EQ(Constants.photoId, photoId));
                                    var m_Update = Update.Set(Constants.status, -1);
                                    var m_SortBy = SortBy.Descending(Constants.photoId);
                                    var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                    cachingObject = BsonSerializer.Deserialize<Photo>(m_Result.ModifiedDocument);
                                    transaction.Complete();
                                }
                                catch (Exception exception)
                                {
                                    new Error().LogError(exception, Constants.photoClass, Constants.approveOrRejectPhotoMethod);
                                    return null;
                                }
                               
                            }
                        }
                    }
                return cachingObject;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.approveOrRejectPhotoMethod);
                return null;
            }
        }




        /// <summary>
        /// get all abusive reported and admin approved photos
        /// </summary>      
        public Photo[] GetAbusiveReportedPhotos()
        {
            try
            {
                AbusiveReport[] abusiveReportObj = new AbusiveReport().GetAllAbusiveReports();
                int[] m_PhotoIds=abusiveReportObj.Select(x=>x.PhotoId).ToArray();
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active photo data from caching db
                var m_Query = Query.And(Query<Photo>.In(e => e.PhotoId, m_PhotoIds),
                                       Query<Photo>.EQ(e => e.Status, 2));
                                     
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getAbusiveReportedPhotosMethod);
                return null;
            }
        }




        /// <summary>
        /// get photos based on status for new requested=0,Reported(Pending)=1,Rejected=-1,Approved=2
        /// </summary>
        public int GetPhotosBasedOnStatusCount(int status)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active photo data from caching db
                var m_Query = Query<Photo>.Where(e => e.Status == status);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return Convert.ToInt32(m_Collection.Find(m_Query).Count());

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getPhotosBasedOnStatusCountMethod);
                return 0;
            }
        }




        /// <summary>
        /// getting admin last approved photos list
        /// </summary>      
        public Photo[] GetAdminLastApprovedPhotos(int adminId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<AdminPhoto>.Where(e => e.AdminId == adminId);
                var m_Collection = cachingDataBase.GetCollection<AdminPhoto>(Constants.adminPhotoClass);
                AdminPhoto[] admPhotoObj = m_Collection.Find(m_Query).OrderBy(e => e._RefID).ToArray();
                List<int> m_newObj = new List<int>();
                foreach (AdminPhoto obj in admPhotoObj)
                {
                    string m_status =new AdminPhoto().GetAdminPhotoLastApprovalStatus(adminId, obj.PhotoId);
                    if (m_status == Constants.c_True)
                    {
                        m_newObj.Add(obj.PhotoId);
                    }
                }
                var m_SelectQuery = Query<Photo>.In(e => e.PhotoId, m_newObj.ToArray());
                var m_NewCollection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_NewCollection.Find(m_SelectQuery).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminPhotoClass, Constants.getAdminLastApprovedPhotosMethod);
                return null;
            }
        }




        /// <summary>
        /// getting  photos array based on user name
        /// to gets the specific user uploaded photos
        /// </summary>          
        public Photo[] GetPhotosBasedOnUserName(string userName)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                int[] m_UserIds = (int[])new User().GetAllUsersByNameSearchNoLimit(userName).Select(e => e._RefID).ToArray();
                var m_Query = Query<Photo>.In(e => e.UserId, m_UserIds);
                var m_Collection = cachingDataBase.GetCollection<Photo>(Constants.photoClass);
                return m_Collection.Find(m_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.photoClass, Constants.getPhotosBasedOnUserNameMethod);
                return null;
            }
        }




    }
}
