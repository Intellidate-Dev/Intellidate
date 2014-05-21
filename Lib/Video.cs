
using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions; 

namespace IntellidateLib
{ // Baseer 

    /// <summary>
    /// The Video Class defines all the generic properties of the video file
    /// </summary>
    public class Video
    {

        /// <summary> 
        /// The Video identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)] 
        public string _id { get; set; }

        /// <summary>
        /// The Video ID from the collection or MySQL Database if any..
        /// </summary>
        public int VideoId { get; set; }
      
        /// <summary>
        /// The User ID from the collection or MySQL Database if any..
        /// </summary>
        public int UserId { get; set; }
       
        /// <summary>
        /// The User's Uploaded Video Title from the collection or MySQL Database if any..
        /// </summary>
        public string VideoTitle { get; set; }
       
        /// <summary>
        /// The User's Uploaded Video path is saved in attachments collections and Attachment id  from the collection or MySQL Database if any..
        /// </summary>
        public int AttachmentId { get; set; }


        public Attachments GetAttachment
        {
            get
            {
                return new Attachments().GetAttachmentById(AttachmentId);
            }
        }
        /// <summary>
        /// The User's Video created date from the collection or MySQL Database if any..
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// The User's Video Duration from the collection or MySQL Database if any..
        /// </summary>
        public string VideoDuration { get; set; }
       
        /// <summary>
        /// The User's Video Size from the collection or MySQL Database if any..
        /// </summary>
        public decimal VideoSize { get; set; }
       
        /// <summary>
        /// The User's Status from the collection or MySQL Database if any..
        /// </summary>
        public string Status { get; set; }




        /// <summary>
        /// The AddNewVideos method. The method must insert into videos collection of both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="Title">The Title  of the Video</param>
        /// <param name="Path">The Path  of the Video</param>
        /// <param name="UserId">The User id  of the user's master</param>
        /// <param name="Duration">The Duration  of the Video</param>
        /// <param name="Size">The Total Size of the Video</param>
        /// <returns>true</returns>
        public bool AddNewVideos(string Title,string Path,int UserId,string Duration,decimal Size)
        {
            try
            {
                  //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Collection = _CachingDatabase.GetCollection<Video>(Constants.videoClass);
                int _VideoId = 0;

                //insert video path into attachment collection
               Attachments o_Attach=new Attachments().AddAttachment("",Path);


              if(o_Attach!=null)
              {
                // insert the Video details in the Main DB and Cache DB
                 using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                 {
                    using (var transaction = new TransactionScope())
                    {
                        in_video_mst _VideoObject = new in_video_mst()
                        {
                            VideoName = Title,
                            AttachmentId   = o_Attach._RefID,
                            UserID = UserId,
                            CreatedDate = DateTime.Now.ToUniversalTime(),
                            Status = Constants.activeStatus,
                            VideoDuration = Duration,
                            VideoSize = Size
                        };
                        _MainDB.in_video_mst.Add(_VideoObject);
                        _MainDB.SaveChanges();
                        _VideoId = _VideoObject.VideoID;
                        try
                        {
                            // Once the insert is done in the MainDB, do the insert on the Caching DB
                            Video _CachingObject = new Video();

                            _CachingObject.VideoId = _VideoId;
                            _CachingObject.UserId = UserId;
                            _CachingObject.VideoDuration = Duration;
                            _CachingObject.AttachmentId = o_Attach._RefID;
                            _CachingObject.VideoSize = Size;
                            _CachingObject.CreatedDate = DateTime.Now.ToUniversalTime();
                            _CachingObject.Status = Constants.activeStatus;
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.videoClass, Constants.addNewVideosMethod);
                            return false;
                        }                    
                    }
                 }
              }
              else
              {
                  return false;
              }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.addNewVideosMethod);
                return false;
            }
        }




        /// <summary>
        /// Getting single user all videos
        /// </summary>
        /// <param name="UserID">User Id from User master</param>
        /// <returns>Video class Array</returns>
        public Video[] GetUserVideos(int UserID)
        {
            try
            {
                //getting data from caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<Video>.Where(e => e.UserId == UserID  & e.Status==Constants.activeStatus);
                var _Collection = CachingDatabase.GetCollection<Video>(Constants.videoClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.getUserVideosMethod);
                return null;
            }
        }




        /// <summary>
        /// Deleting videos based on videoid it will not delete entaire record it will update the status as InActive mode.
        /// </summary>
        /// <param name="VideoId">id of a single video</param>
        /// <returns>true or false</returns>
        public bool DeleteVideos(int VideoId)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Video we just updates the status of video A(Active) to I(Inactive)
                var _SelectQuery = Query<Video>.EQ(e => e.VideoId, VideoId);
                var _Collection = _CachingDatabase.GetCollection<Video>(Constants.videoClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                     
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_video_mst _VideoObj = _MainDB.in_video_mst.SingleOrDefault(c => c.VideoID == VideoId);
                            _VideoObj.Status = Constants.inActiveStatus;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Video _CachingObject = new Video();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.videoId, VideoId));
                                var _Update = Update.Set(Constants.status, Constants.inActiveStatus);
                                var _SortBy = SortBy.Descending(Constants.videoId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.videoClass, Constants.deleteVideosMethod);
                                return false;
                            }
                            
                        }
                    }
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.deleteVideosMethod);
                return false;
            }
        }




        /// <summary>
        /// Returns this mounth adeded videos as an Array
        /// </summary>
        /// <returns>Video Class Array</returns>
        public Video[] GetThisMonthVideos()
        {

            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting data from caching db using defferences between currentdate and current month start date
                var _Query = Query<Video>.Where(x => x.CreatedDate >= DateTime.Today.AddDays(1 - DateTime.Today.Day) & x.CreatedDate <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<Video>(Constants.videoClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.getThisMonthVideosMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns this Week adeded Videos as an Array
        /// </summary>
        /// <returns>Video Class Array</returns>
        public Video[] GetThisWeekVideos()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting Current week start date
                DateTime _WeekStart = DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek);
                //Getting this week Saved Videos details from caching db using 'deffrences between current week start date and current date
                var _Query = Query<Video>.Where(x => x.CreatedDate >= _WeekStart & x.CreatedDate <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<Video>(Constants.videoClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.getThisWeekVideosMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns today adeded Videos as an Array
        /// </summary>
        /// <returns>Video Class Array</returns>
        public Video[] GetThisDayVideos()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting Todays saved video details from caching db 
                var _Query = Query<Video>.Where(x => x.CreatedDate >= DateTime.Today & x.CreatedDate <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<Video>(Constants.videoClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.getThisDayVideosMethod);
                return null;
            }
        }
       
        
        
        
        /// <summary>
        /// Returns total space ocupied by a single user for videos
        /// </summary>
        /// <param name="UserID">The User Id in user collection</param>
        /// <returns>decimal size in kbs</returns>
        public decimal GetUserUsedSpaceForVideos(int UserID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //query for video collection
                var _Query = Query<Video>.Where(e => e.Status == Constants.activeStatus & e.UserId == UserID);
                var _Collection = CachingDatabase.GetCollection<Video>(Constants.videoClass);
                //to return the sum of the videos size for the selected user
                return _Collection.Find(_Query).Sum(c => c.VideoSize);
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.videoClass, Constants.getUserUsedSpaceForVideosMethod);
                return 0;
            }
        }
   
    
    
    
    }
}
